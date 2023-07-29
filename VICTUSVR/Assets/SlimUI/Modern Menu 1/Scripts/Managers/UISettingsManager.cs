using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

namespace SlimUI.ModernMenu{
	public class UISettingsManager : MonoBehaviour {

		public enum Platform {Desktop, Mobile};
		public Platform platform;

		[Header("GAME SETTINGS")]
		public GameObject difficultynormaltext;
		public GameObject difficultynormaltextLINE;
		public GameObject difficultyhardcoretext;
		public GameObject difficultyhardcoretextLINE;

		// sliders
		public GameObject musicSlider; 

		private float sliderValue = 0.0f;
		private float sliderValueXSensitivity = 0.0f;
		private float sliderValueYSensitivity = 0.0f;
		private float sliderValueSmoothing = 0.0f;
		

		public void  Start (){
			// check difficulty
			if(PlayerPrefs.GetInt("NormalDifficulty") == 1){
				difficultynormaltextLINE.gameObject.SetActive(true);
				difficultyhardcoretextLINE.gameObject.SetActive(false);
			}
			else
			{
				difficultyhardcoretextLINE.gameObject.SetActive(true);
				difficultynormaltextLINE.gameObject.SetActive(false);
			}

			// check slider values
			musicSlider.GetComponent<Slider>().value = PlayerPrefs.GetFloat("MusicVolume");

		}

		public void Update (){
			//sliderValue = musicSlider.GetComponent<Slider>().value;
		}

		public void MusicSlider (){
			//PlayerPrefs.SetFloat("MusicVolume", sliderValue);
			PlayerPrefs.SetFloat("MusicVolume", musicSlider.GetComponent<Slider>().value);
		}

		// public void SensitivitySmoothing (){
		// 	PlayerPrefs.SetFloat("MouseSmoothing", sliderValueSmoothing);
		// 	Debug.Log(PlayerPrefs.GetFloat("MouseSmoothing"));
		// }


		// // the playerprefs variable that is checked to enable mobile sfx while in game
		// public void MobileSFXMute (){
		// 	if(PlayerPrefs.GetInt("Mobile_MuteSfx")==0){
		// 		PlayerPrefs.SetInt("Mobile_MuteSfx",1);
		// 		mobileSFXtext.GetComponent<TMP_Text>().text = "on";
		// 	}
		// 	else if(PlayerPrefs.GetInt("Mobile_MuteSfx")==1){
		// 		PlayerPrefs.SetInt("Mobile_MuteSfx",0);
		// 		mobileSFXtext.GetComponent<TMP_Text>().text = "off";
		// 	}
		// }

		// public void MobileMusicMute (){
		// 	if(PlayerPrefs.GetInt("Mobile_MuteMusic")==0){
		// 		PlayerPrefs.SetInt("Mobile_MuteMusic",1);
		// 		mobileMusictext.GetComponent<TMP_Text>().text = "on";
		// 	}
		// 	else if(PlayerPrefs.GetInt("Mobile_MuteMusic")==1){
		// 		PlayerPrefs.SetInt("Mobile_MuteMusic",0);
		// 		mobileMusictext.GetComponent<TMP_Text>().text = "off";
		// 	}
		// }

		public void NormalDifficulty (){
			difficultyhardcoretextLINE.gameObject.SetActive(false);
			difficultynormaltextLINE.gameObject.SetActive(true);
			PlayerPrefs.SetInt("NormalDifficulty",1);
			PlayerPrefs.SetInt("HardCoreDifficulty",0);
		}

		public void HardcoreDifficulty (){
			difficultyhardcoretextLINE.gameObject.SetActive(true);
			difficultynormaltextLINE.gameObject.SetActive(false);
			PlayerPrefs.SetInt("NormalDifficulty",0);
			PlayerPrefs.SetInt("HardCoreDifficulty",1);
		}

	}
}