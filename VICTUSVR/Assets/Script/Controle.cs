using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; //Elementos da Interface Gráfica
using UnityEngine.AI;
using System.IO.Ports; // Biblioteca para ler comunicação serial com Arduino
using System.Threading;
using System.Globalization;

public class Controle : MonoBehaviour {


	private static SerialPort serial;

	public Text displayContagem, displayBatimentos, displayVelocidade, displayEmg, displayObjetivo;
	public float tempoSegundos, tempoMinutos, tempo=0.0f;
	public float fimDaPartida=0.0f;
	public NavMeshAgent navmesh;
	public GameObject player;
	public Text tempoFim;
	public GameObject entrada;
	public AudioSource bike, musica;
	public GameObject InputField;
	Thread IOThread = new Thread (DataThread);

	string emg, bpm;// variáveis 
	public int velInt, direcao;
	public float velocidade, eixo;

	private static void DataThread(){
		serial = new SerialPort ("COM6", 115200);
		serial.Open ();
			Thread.Sleep(200);
		}

	private void OnDestroy(){
		IOThread.Abort ();
		serial.Close ();
	}


	// Use this for initialization
	void Start () {
		IOThread.Start();
		Time.timeScale = 0;
		// serial.ReadTimeout = -1;
		// navmesh = player.GetComponent<NavMeshAgent> ();
		//StartCoroutine (LerDadosDoSerial ());// começa o loop
		displayVelocidade.text = "0";
		displayBatimentos.text = " ";
		//fimDaPartida = getda interface

	}


	// Update is called once per frame
	void Update () {
			if (tempo >= fimDaPartida) {
			//fim da partida
				displayObjetivo.text = "Acabou a sessão!";
				Time.timeScale = 0;
			bike.Pause ();
			musica.Pause ();
				//******INSTANTIATE OS SCORES
			} else {

			tempo += Time.deltaTime;
			tempoMinutos = ((int)tempo / 60) ;
			tempoSegundos = (int)tempo % 60;
			displayContagem.text = tempoMinutos.ToString("00")+ ":"+tempoSegundos.ToString("00");
			//Debug.Log("Em cima" + serial.ReadLine());
		/*if (velocidade > 0) {	
			velocidade= velocidade - 0.15f;
			displayVelocidade.text = velocidade.ToString ("00");
			navmesh.speed = (float)(velocidade / 3.6);
		}*/
			//
			// No arduino connected
			// 
			string[] valores = serial.ReadLine().Split ('#'); // separador de valores
			// Debug.Log("Valores" + "#"+valores[0] +"#"+ valores[1] +"#"+ valores[2] );
			while(valores.Length < 4){ // to avoid errors
				valores = serial.ReadLine ().Split ('#'); // separador de valores
				serial.BaseStream.Flush(); //Clear the serial information so we assure we get new information.

			}
			bpm = valores[0];
			velocidade = float.Parse(valores [1], CultureInfo.InvariantCulture);
			emg = valores [2];
			direcao = int.Parse(valores[3]);
			//eixo = valores [3];
			// LerDadosDoSerial();
			Debug.Log("Valores" + "#"+bpm +"#"+ velocidade +"#"+ emg + "#"+ direcao);
			displayBatimentos.text = bpm;
			displayEmg.text = "EMG: "+emg;
			// navmesh.speed = (float)(velocidade / 3.6); 
			velInt = (int)velocidade;
			serial.BaseStream.Flush(); //Clear the serial information so we assure we get new information.
		}
	}

	public int getDirecao(){
		return direcao;
	}	

	public float getVelocidade(){
		return velocidade;
	}
		
	IEnumerator LerDadosDoSerial(){
		while(true){
			string[] valores = serial.ReadLine ().Split ('#'); // separador de valores BPM#Vel#EMG
			bpm = valores[0];
			velocidade = float.Parse(valores [1]);
			emg = valores [2];
			//eixo = ventradaalores [3];
			Debug.Log("Valores: " + bpm + "#" + velocidade + "#" + emg);
			serial.BaseStream.Flush(); //Clear the serial information so we assure we get new information.
			yield return new WaitForSeconds (0.3f); //tempo de leitura de novas informações

		}
	}

	public void SetaTempo(){
		if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)){
			fimDaPartida =60* float.Parse(tempoFim.text);
			Time.timeScale = 1;
			Debug.Log("passei pelo setar tempo\n");	
			entrada.SetActive (false);
			// musica.Play ();
			// bike.Play ();
		}
	}

}