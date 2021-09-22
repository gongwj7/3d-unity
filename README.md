# **unity实现井字棋小游戏**

## **前言**
这是中山大学2021年3D游戏编程与设计的第一次作业

## **游戏简介**
井字棋游戏，有pvp和pve两种模式供选择，并带有背景音乐

## **游戏截图**
![](picture/demo1.png) 

## **实现过程**
**Reset():**  
棋盘初始化函数，将棋盘所有格恢复为初始状态。 
```c#
void Reset () {
		turn = 1;
		for (int i = 0; i < 3; i++) {
			for (int j = 0; j < 3; j++) {
				Chessboard [i] [j] = 0;
			}
		}	
		audioSource.Play ();
	}
``` 
**check():**   
用于判断游戏状态
行获胜
```c#
		for (int i = 0; i < 3; i++) {
			if (Chessboard[i][0] != 0 && Chessboard[i][0] == Chessboard[i][1] && Chessboard[i][1] == Chessboard[i][2]) {
				return Chessboard[i][0];
			}
		}
```
列获胜
```c#
		for (int i = 0; i < 3; i++) {
			if (Chessboard[0][i] != 0 && Chessboard[0][i] == Chessboard[1][i] && Chessboard[1][i] == Chessboard[2][i]) {
				return Chessboard[0][i];
			}
		}
```
 斜线获胜
```c#
		if (Chessboard[1][1] != 0 &&
		    Chessboard[0][0]== Chessboard[1][1] && Chessboard[2][2] == Chessboard[1][1] ||
		    Chessboard[0][2] == Chessboard[1][1] && Chessboard[2][0] == Chessboard[1][1]) {
			return Chessboard[1][1];
		}
```
  对局继续
```c#
		for (int i = 0; i < 3; ++i) {
			for (int j = 0; j < 3; ++j) {
				if (Chessboard[i][j] == 0) return 0;
			}
		}
```
  平局
```c#
		return 0;
```
**pvp():**  
先绘制已经下好的棋子，再判断游戏是否结束，若没结束则根据turn的值进行本次点击，更改多维数组的值
```c#
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
```
**pve():**  
PVE对抗函数，逻辑与PVP相似，只是在AI回合时会调用AITurn()进行落子。
```c#
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
```
**AITurn():**  
AI落子函数，在这个函数中AI会比较简单的选择局部最优的选择，选择逻辑如下。  
1. **能够获胜则直接落子**  
对每个空白格进行检测，判断落子后是否可以获胜，若可以，则直接落子。  
2. **若不能获胜，且玩家已将军，则进行防守**  
判断玩家是否将军，若将军，则记录应该防守的位置
若所有空白格检测完毕后，不能获胜，则选择记下的位置进行落子。

3. **如果既不能获胜，玩家也没有将军，则随机选择一个空白格**  
在遍历时加入一个变量count，记录空白格的数量，与一个多维数组ept[][]，来记录空白格的位置
```c#
	void ai_turn() {
		//如果没有在运行中则不进行下一步
		if (check() != 0)
			return;
		//使用交错数组存储空白位置
		int[][] ept = new int[3,3];
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
		int rd = (int)Random.Range(0, count);
		/*int rd = 0;
		for (int i = 0; i < 3; i++) {
			for(int j = 0; j < 3; j++){
				if(ept[i,j] == rd)
					Chessboard[i][j] = 2;
			}
		}
		*/
    int i1 = ept[rd][0];
    int j1 = ept[rd][1];
    Chessboard[i1][j1] = 2; 
```
**audiosource**
设置AudioSource组件
首先选择Add component
![](picture/演示1.png) 
而后选择Audio Sourse
![](picture/演示2.png) 
最后选择素材就ok了~
![](picture/演示3.png) 
```c#
private void Awake() {
		audioSource= this.gameObject.GetComponent<AudioSource>();
		audioSource.loop = false;
		audioSource.volume = 1.0f;
		audioSource.clip = audio;
	}
```


