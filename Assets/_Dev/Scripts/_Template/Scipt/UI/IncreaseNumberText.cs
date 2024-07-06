using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Template.Script.UI
{
    public class IncreaseNumberText:MonoBehaviour
    {
        [SerializeField] float _duration;
        private TextMeshProUGUI m_Text;
        private Coroutine m_Coroutine;

        internal void Initm_TextProUGUI(TextMeshProUGUI txt)
        {
            m_Text = txt;
        }
        public void SetNum(int num, float duration=-1)
        {
            if(m_Coroutine!=null)
            {
                StopCoroutine(m_Coroutine);
            }
            Coroutine co = StartCoroutine(SetNumAsync(num, duration));
            m_Coroutine = co;
        }

        private IEnumerator SetNumAsync( int num,float duration=-1)
        {
            if (duration == -1) duration = _duration;
            float spentTime = Time.deltaTime;
            int displayNum;
            if (!int.TryParse(m_Text.text, out displayNum) || duration<=0)
            {
                m_Text.text = num.ToString();
            }
            else
            {
                while (spentTime < duration)
                {
                    displayNum = (int)Mathf.Lerp(displayNum, num, spentTime / duration);
                    m_Text.SetText(displayNum.ToString());
                    spentTime += Time.deltaTime;
                    yield return null;
                }
                if (spentTime > duration)
                {
                    displayNum = num;
                    m_Text.SetText(displayNum.ToString());
                }
            }
        }
    }
}
