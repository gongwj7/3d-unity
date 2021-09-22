using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TTT : MonoBehaviour {
	public Texture2D img;
	public Texture2D img1;
	public Texture2D img2;
	public AudioClip audio;
	private AudioSource audioSource;
	private int turn = 1;
	private int mode = 1;
	GUIStyle myStyle;
	int[][] Chessboard = new int[3][] { new int[3], new int[3], new int[3] };

	void Reset () {
		turn = 1;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				Chessboard [i] [j] = 0;
			}
		}	
		audioSource.Play ();
	}

	// Use this for initialization
	void Start () {
		Reset ();		
	}

	int check() {
		
		for (int i = 0; i < 3; i++) {
			if (Chessboard[i][0] != 0 && Chessboard[i][0] == Chessboard[i][1] && Chessboard[i][1] == Chessboard[i][2]) {
				return Chessboard[i][0];
			}
		}
		
		for (int i = 0; i < 3; i++) {
			if (Chessboard[0][i] != 0 && Chessboard[0][i] == Chessboard[1][i] && Chessboard[1][i] == Chessboard[2][i]) {
				return Chessboard[0][i];
			}
		}
		
		if (Chessboard[1][1] != 0 &&
		    Chessboard[0][0]== Chessboard[1][1] && Chessboard[2][2] == Chessboard[1][1] ||
		    Chessboard[0][2] == Chessboard[1][1] && Chessboard[2][0] == Chessboard[1][1]) {
			return Chessboard[1][1];
		}
		
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				if (Chessboard[i][j] == 0) return 0;
			}
		}
		
		return 0;
	}

	private void Awake() {
		audioSource= this.gameObject.GetComponent<AudioSource>();
		audioSource.loop = false;
		audioSource.volume = 1.0f;
		audioSource.clip = audio;
	}

	void OnGUI () {
		GUI.BeginGroup (new Rect (Screen.width / 2 - 50, Screen.height / 2 - 50, 400, 400));
		GUI.Box (new Rect (0,0,400,400),"Tic-Tac-Toe");
		//GUI.EndGroup ();
		//GUI.Label (new Rect (0, 0, Screen.width, Screen.height), img);
		/*
		GUI.Button (new Rect (5,20,30,30), "");
		GUI.Button (new Rect (35,20,30,30), "");
		GUI.Button (new Rect (65,20,30,30), "");
		GUI.Button (new Rect (5,50,30,30), "");
		GUI.Button (new Rect (35,50,30,30), "");
		GUI.Button (new Rect (65,50,30,30), "");
		GUI.Button (new Rect (5,80,30,30), "");
		GUI.Button (new Rect (35,80,30,30), "");
		GUI.Button (new Rect (65,80,30,30), "");
		GUI.EndGroup ();

		int result = check();

		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (Chessboard [i][j] == 1)
					GUI.Button (new Rect (5 + 30 * i, 20 + 30*j, 30, 30), img1);
				if (Chessboard [i][j] == 2)
					GUI.Button (new Rect (5 + 30 * i, 20 + 30*j, 30, 30), img2);
				if (GUI.Button (new Rect (5 + 30 * i, 20 + 30*j, 30, 30), "")){
					 /*按下按钮，切换玩家*/
		/*
					if(result == 0){
						if (turn == 1){
							Chessboard[i][j] = 1;
							turn = 2;
						}
						else{
							Chessboard[i][j] = 2;
							turn = 1;
						}
					}
				}
			}
		}
	*/
		
		//加载选择与人或与AI对战模式的选项及实现其功能
		if (GUI.Button (new Rect (200, 50, 100, 50), new GUIContent ("reset", "点击重新开始"))) {
			Reset ();
		}
		if (GUI.Button(new Rect(200, 100, 100, 50), "player vs player")){
			mode = 1;
			Reset();
		}
		if (GUI.Button(new Rect(200, 150, 100, 50), "player vs AI")){
			mode = 2;
			Reset();
		}
		int res = check ();
		if (res == 0) {
			if (turn == 1)
				GUI.Box(new Rect(300, 100, 100, 50), "p1 is going");
			else 
				GUI.Box(new Rect(300, 100, 100, 50), "p2(ai) is going");
		}
		else if (res == 1) {
			GUI.Box(new Rect(300, 100, 100, 50), "p1 WIN");
		}
		else if (res == 2) {
			GUI.Box(new Rect(300, 100, 100, 50), "p2(ai) WIN");
		}
		else if (res == 3) {
			GUI.Box(new Rect(300, 100, 100, 50), "Draw");
		}
		
		//游戏运行逻辑
		//mode1: 人vs人
		//mode2: 人vsAI
		if (mode == 1)
			pvp();
		else
			pve();


		GUI.EndGroup ();
	}

	void pvp(){
		int result = check();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (Chessboard [i][j] == 1)
					GUI.Button (new Rect (5 + 60 * i, 20 + 60*j, 60, 60), img1);
				if (Chessboard [i][j] == 2)
					GUI.Button (new Rect (5 + 60 * i, 20 + 60*j, 60, 60), img2);
				if (GUI.Button (new Rect (5 + 60 * i, 20 + 60*j, 60, 60), "")){
					/* 按下按钮，切换玩家*/
					if(result == 0){
						if (turn == 1){
							Chessboard[i][j] = 1;
							turn = 2;
						}
						else{
							Chessboard[i][j] = 2;
							turn = 1;
						}
					}
				}
			}
		}
	}

	void pve(){
		int result = check();
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (Chessboard [i][j] == 1)
					GUI.Button (new Rect (5 + 60 * i, 20 + 60*j, 60, 60), img1);
				if (Chessboard [i][j] == 2)
					GUI.Button (new Rect (5 + 60 * i, 20 + 60*j, 60, 60), img2);
				if (GUI.Button (new Rect (5 + 60 * i, 20 + 60*j, 60, 60), "")){
					/* 按下按钮，切换玩家*/
					if(result == 0){
						if (turn == 1){
							Chessboard[i][j] = 1;
							turn = 2;
						}
						else{
							ai_turn();
							turn = 1;
						}
					}
				}
			}
		}
	}

	void ai_turn() {
		//如果没有在运行中则不进行下一步
		if (check() != 0)
			return;
		//使用交错数组存储空白位置
		int[,] ept = new int[3,3];
		int lose_x = -1;
		int lose_y = -1;
		//int[] ept = new int[9];
		int count = 0;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				if (Chessboard [i][j] == 0) {
					//判断是否能赢
					Chessboard [i][j] = 2;
					if (check () == 2)
						return;

					//判断是否被将军
					Chessboard [i][j] = 1;
					if (check () == 1) {
						lose_x = i;
						lose_y = j;
					}
					//恢复chessboard的值
					Chessboard [i][j] = 0;
					ept[i,j] = count;
					count++;
				}
			}
		}
		//若将军则落子
		if(lose_x != -1)
			Chessboard[lose_x][lose_y] = 2;
			return;

		//int[][] ept = new int[count][] { new int[2], new int[2], new int[2] };
		//无法直接获胜也没被将军则随机在空白格落子
		//int rd = (int)Random.Range(0, count);
		int rd = 0;
		for (int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++){
				if(ept[i,j] == rd)
					Chessboard[i][j] = 2;
			}
		}
		//Chessboard[i1][j1] = 2; 


	}
}
