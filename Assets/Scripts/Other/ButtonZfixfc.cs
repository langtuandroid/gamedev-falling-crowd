using UnityEngine;

namespace Other
{
	public class ButtonZfixfc : MonoBehaviour
	{
		[SerializeField]
		private float scaleAnimFc;
		[SerializeField]
		private float speedAnimfc;
		
		private float scalefc;
		private float startScalefc;
		private int stepfc;
		
		private void Start ()
		{
			scalefc = transform.localScale.x;
			startScalefc = scalefc;

			if (speedAnimfc == 0) speedAnimfc = 0.3f;
			if (scaleAnimFc == 0) scaleAnimFc = 1.2f;

			stepfc = 1;
		}
		
		private void Update ()
		{
			if (stepfc == 0)
			{
				scalefc += Time.deltaTime * speedAnimfc;
				transform.localScale = new Vector3(scalefc,scalefc,1);

				if (scalefc > startScalefc * scaleAnimFc) stepfc = 1;
			}
			else
			{
				scalefc -= Time.deltaTime * speedAnimfc;
				transform.localScale = new Vector3(scalefc,scalefc,1);
			
				if (scalefc < startScalefc) stepfc = 0;
			}
		}
	}
}
