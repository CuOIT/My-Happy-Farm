using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Animal : MonoBehaviour,IAnimal
{
    [SerializeField] Transform[] movePositions; // Positions move to
    [SerializeField] float moveSpeed = 2f; // Speed of movement
    private Animator animator; // Animator component

    private enum State { Idle, Moving, Eating, Jumping }
    private State currentState = State.Idle;

    private Transform targetPosition;
    private float idleDuration = 3f;
    private float eatDuration = 1f;
    private float jumpDuration = 3f;

    private Coroutine currentCoroutine;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(StateMachine());
    }

    void StartStateMachine()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine(StateMachine());
    }

    IEnumerator StateMachine()
    {
        while (true)
        {
            switch (currentState)
            {
                case State.Idle:
                    currentCoroutine = StartCoroutine(IdleState());
                    break;
                case State.Moving:
                    currentCoroutine = StartCoroutine(MovingState());
                    break;
                case State.Eating:
                    currentCoroutine = StartCoroutine(EatingState());
                    break;
                case State.Jumping:
                    currentCoroutine = StartCoroutine(JumpingState());
                    break;
            }
            yield return currentCoroutine;
        }
    }

    IEnumerator IdleState()
    {
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(idleDuration);
         targetPosition = movePositions[Random.Range(0, movePositions.Length)];
        currentState = State.Moving;
        yield return null;
    }

    IEnumerator MovingState()
    {
        animator.SetTrigger("Move");
        Vector3 direction = (targetPosition.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
        while (Vector3.Distance(transform.position, targetPosition.position) > 0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition.position, moveSpeed * Time.deltaTime);
            yield return null;
        }
        currentState = State.Eating;
    }

    IEnumerator EatingState()
    {
        animator.SetTrigger("Eat");

        yield return new WaitForSeconds(eatDuration);
        currentState = State.Idle;
    }

    IEnumerator JumpingState()
    {
        animator.SetTrigger("Jump");
        yield return new WaitForSeconds(jumpDuration);
        currentState = State.Idle;
    }

    public void Feed()
    {
        if (currentCoroutine != null)
        {
            StopCoroutine(currentCoroutine);
        }
        currentState = State.Jumping;
        StartStateMachine();
        ShowMessage("YUMMYYY");
    }

    private Coroutine currentText;

    public void ShowHungry()
    {
        ShowMessage("I'm so hungry T.T");
    }

    public void Greet()
    {
        ShowMessage("I'm still full");
    }

    [SerializeField] TextMeshProUGUI txt;
    [SerializeField] GameObject canvas;
    IEnumerator ShowText(string text)
    {
        canvas.SetActive(true);
        txt.SetText(text);
        yield return new WaitForSeconds(2.5f);
        canvas.SetActive(false);
    }

    void ShowMessage(string text)
    {
        if(currentText != null)
        {
            StopCoroutine(currentText);
        }
        currentText = StartCoroutine(ShowText(text));   
    }
    public void OnFeed()
    {
        Feed();
    }
}
