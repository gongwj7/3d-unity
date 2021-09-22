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
**checkState():**   
用于检测当前游戏状态，判断游戏是否进行中或者已经停止。  
判断逻辑比较简单，首先判断对角线棋子是否全部相同。  
通过遍历的方式同时判断行与列的棋子是否相同，以及统计空白格的数量，提高运行效率。
**playerVsPlayer():**  
PVP对抗函数，以遍历整个棋盘的方式，判断玩家是否落子。  
探测到玩家落子后，将棋盘相对应的数据标记为玩家代号，储存玩家身份信息并表示玩家已落子。  
每次都会根据棋盘的信息来显示相应的图片(O,X或空白)
**playerVsAIMode():**  
PVE对抗函数，逻辑与PVP相似，只是在AI回合时会调用AITurn()进行落子。
**AITurn():**  
AI落子函数，在这个函数中AI会比较简单的选择局部最优的选择，选择逻辑如下。  
1. **如果可以获胜，则获胜**  
对每个空白格进行检测，判断落子后是否可以获胜，若可以，则直接落子。  
2. **如果不能获胜，且玩家已将军，则堵上玩家的棋子**  
在步骤1的基础上，判断空白格如果是玩家落子后，玩家是否会获胜，若玩家会获胜，说明玩家已将军，记下当前位置。  
若所有空白格检测完毕后，不能获胜，则选择记下的位置进行落子。  
3. **如果既不能获胜，玩家也没有将军，则随机选择一个空白格**  
在对步骤1, 2进行检测的时候，用mp记下空白格的数量以及空白格的数量cnt。  
如果步骤1, 2均不能满足，则产生一个0-cnt之间的随机数rd，从mp取出rd对应的棋子位置，进行落子。  
**audiosource**