using Godot;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

public partial class main : Node2D
{
	TextureButton Button1;
	TextureButton Button2;
	TextureButton Button3;
	TextureButton Button4;
	TextureButton Button5;
	TextureButton Button6;
	TextureButton Button7;
	TextureButton Button8;
	TextureButton Button9;
	Button chgPlayerNum;

	Label gameStatus;
	//玩家1图片
	Texture2D player1Texture;
	//玩家2图片
	Texture2D player2Texture;
	//空白图片
	Texture2D spaceTexture;

	Texture2D plyer1disTexture;
	Texture2D plyer2disTexture;



	//二维数组,用于存储棋盘上的棋子,0为空,1为玩家1,2为玩家2
	int[,] chess = new int[3, 3];

	//下棋次序，1为玩家1,2为玩家2
	int order = 1;
	//玩家最多棋子数
	int maxChess = 3;
	//玩家1棋子数
	int player1Chess = 0;
	//玩家1棋子次序
	List<(int, int)> player1ChessOrder = new List<(int, int)>();
	//玩家2棋子次序
	List<(int, int)> player2ChessOrder = new List<(int, int)>();
	//玩家2棋子数
	int player2Chess = 0;
	//是否结束
	bool isGameOver = false;
	//玩家1或者2是否胜利，1为玩家1,2为玩家2
	int winPlayer = 0;
	//玩家人数
	int playerNum = 2;
	//游戏状态,0为准备,1为游戏中,2为结束
	int gameStatusNum = 0;

	public override void _Ready()
	{
		GD.Print("Hello, World!");
		//获取节点
		Button1 = GetNode<TextureButton>("Control/GridContainer/Button1");
		Button2 = GetNode<TextureButton>("Control/GridContainer/Button2");
		Button3 = GetNode<TextureButton>("Control/GridContainer/Button3");
		Button4 = GetNode<TextureButton>("Control/GridContainer/Button4");
		Button5 = GetNode<TextureButton>("Control/GridContainer/Button5");
		Button6 = GetNode<TextureButton>("Control/GridContainer/Button6");
		Button7 = GetNode<TextureButton>("Control/GridContainer/Button7");
		Button8 = GetNode<TextureButton>("Control/GridContainer/Button8");
		Button9 = GetNode<TextureButton>("Control/GridContainer/Button9");
		gameStatus = GetNode<Label>("Control/gameStatus");
		chgPlayerNum = GetNode<Button>("Control/chgPlayerNum");
		//加载图片
		player1Texture = GD.Load<Texture2D>("res://asset/player1.png");
		player2Texture = GD.Load<Texture2D>("res://asset/player2.png");
		spaceTexture = GD.Load<Texture2D>("res://asset/space.png");
		plyer1disTexture = GD.Load<Texture2D>("res://asset/player1-dis.png");
		plyer2disTexture = GD.Load<Texture2D>("res://asset/player2-dis.png");
		//初始化棋盘
		initChess();
	}

	public override void _Process(double delta)
	{
		//显示最大数量判断
		judgeMaxChess();
		//更新棋盘
		updateChess();
		//显示游戏状态
		showGameStatus();
	}
	//开始游戏
	public void startGame()
	{
		if (gameStatusNum == 0)
		{
			gameStatusNum = 1;
		}
	}
	//重置游戏
	public void resetGame()
	{
		initChess();
		isGameOver = false;
		winPlayer = 0;
		order = 1;
		player1Chess = 0;
		player2Chess = 0;
		gameStatusNum = 0;
		player1ChessOrder.Clear();
		player2ChessOrder.Clear();
	}
	//初始化棋盘
	public void initChess()
	{
		for (int i = 0; i < 3; i++)
		{
			for (int j = 0; j < 3; j++)
			{
				chess[i, j] = 0;
			}
		}
	}

	//显示游戏状态
	public void showGameStatus()
	{
		if (isGameOver)
		{
			gameStatusNum = 2;
		}
		if (gameStatusNum == 0)
		{
			gameStatus.Text = "准备中";
		}
		else if (gameStatusNum == 1)
		{
			if (order == 1)
			{
				gameStatus.Text = "玩家1下";
			}
			else
			{
				gameStatus.Text = "玩家2下";
			}
		}
		else if (gameStatusNum == 2)
		{
			if (winPlayer == 1)
			{
				gameStatus.Text = "玩家1胜利";
			}
			else if (winPlayer == 2)
			{
				gameStatus.Text = "玩家2胜利";
			}
		}
	}

	//判断是否结束
	public void judgeGameOver()
	{
		//判断行
		for (int i = 0; i < 3; i++)
		{
			if (chess[i, 0] == chess[i, 1] && chess[i, 1] == chess[i, 2] && chess[i, 0] == 1)
			{
				isGameOver = true;
				winPlayer = 1;
				return;
			}
			if (chess[i, 0] == chess[i, 1] && chess[i, 1] == chess[i, 2] && chess[i, 0] == 2)
			{
				isGameOver = true;
				winPlayer = 2;
				return;
			}
		}
		//判断列
		for (int i = 0; i < 3; i++)
		{
			if (chess[0, i] == chess[1, i] && chess[1, i] == chess[2, i] && chess[0, i] == 1)
			{
				isGameOver = true;
				winPlayer = 1;
				return;
			}
			if (chess[0, i] == chess[1, i] && chess[1, i] == chess[2, i] && chess[0, i] == 2)
			{
				isGameOver = true;
				winPlayer = 2;
				return;
			}
		}
		//判断对角线
		if (chess[0, 0] == chess[1, 1] && chess[1, 1] == chess[2, 2] && chess[0, 0] == 1)
		{
			isGameOver = true;
			winPlayer = 1;
			return;
		}
		if (chess[0, 0] == chess[1, 1] && chess[1, 1] == chess[2, 2] && chess[0, 0] == 2)
		{
			isGameOver = true;
			winPlayer = 2;
			return;
		}
		if (chess[0, 2] == chess[1, 1] && chess[1, 1] == chess[2, 0] && chess[0, 2] == 1)
		{
			isGameOver = true;
			winPlayer = 1;
			return;
		}
		if (chess[0, 2] == chess[1, 1] && chess[1, 1] == chess[2, 0] && chess[0, 2] == 2)
		{
			isGameOver = true;
			winPlayer = 2;
			return;
		}
	}

	//下棋
	public void playChess(int x, int y)
	{

		if (gameStatusNum == 0||gameStatusNum==2)
		{
			return;
		}
		if (chess[x, y] == 0)
		{
			if (order == 1)
			{
				chess[x, y] = 1;
				player1Chess++;
				player1ChessOrder.Add((x, y));
				if (player1Chess > maxChess)
				{
					chess[player1ChessOrder[0].Item1, player1ChessOrder[0].Item2] = 0;
					player1ChessOrder.RemoveAt(0);
					player1Chess--;
				}
				judgeGameOver();
				order = 2;
				if (playerNum == 1)
				{
					AIPlayChess();
				}
			}
			else
			{
				if (playerNum == 2)
				{
					chess[x, y] = 2;
					player2Chess++;
					player2ChessOrder.Add((x, y));
					if (player2Chess > maxChess)
					{
						chess[player2ChessOrder[0].Item1, player2ChessOrder[0].Item2] = 0;
						player2ChessOrder.RemoveAt(0);
						player2Chess--;
					}
					order = 1;
				}
			}
		}
		judgeGameOver();
	}
	//AI下棋
	public void AIPlayChess()
	{
		if (gameStatusNum == 0||gameStatusNum==2)
		{
			return;
		}
		if (order == 2)
		{
			//TODO AI下棋,通过判断每个点分值来下棋
			//判断分值：
			//1.判断是否可以赢，如果可以赢则100分；
			//2.判断是否可以防守，如果可以防守则90分；
			//3.判断是否可以形成两个棋子，如果可以形成两个棋子则80分；
			//4.判断是否可以形成一个棋子，如果可以形成一个棋子则70分；
			//5.判断是否可以形成两个空位，如果可以形成两个空位则60分；
			//6.判断是否可以形成一个空位，如果可以形成一个空位则50分；
			var score = new int[3, 3];
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (chess[i, j] == 0)
					{
						if (aiJudgeWin(i, j))
						{
							score[i, j] = 100;
						}
						else if (aiJudgeDefend(i, j))
						{
							score[i, j] = 90;
						}
						else if (aiJudgeTwoChess(i, j))
						{
							score[i, j] = 80;
						}
						else if (aiJudgeOneChess(i, j))
						{
							score[i, j] = 70;
						}
						else if (aiJudgeTwoSpace(i, j))
						{
							score[i, j] = 60;
						}
						else if (aiJudgeOneSpace(i, j))
						{
							score[i, j] = 50;
						}
						else
						{
							score[i, j] = 0;
						}

					}
				}
			}
			//找出最大分值
			int maxScore = 0;
			int maxX = 0;
			int maxY = 0;
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (score[i, j] > maxScore)
					{
						maxScore = score[i, j];
						maxX = i;
						maxY = j;
					}
				}
			}
			//找出所有最大值
			List<(int, int)> maxList = new List<(int, int)>();
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (score[i, j] == maxScore)
					{
						maxList.Add((i, j));
					}
				}
			}
			//随机选择一个最大值
			Random random = new Random();
			int index = random.Next(0, maxList.Count);
			maxX = maxList[index].Item1;
			maxY = maxList[index].Item2;
			chess[maxX, maxY] = 2;

			player2Chess++;
			player2ChessOrder.Add((maxX, maxY));
			if (player2Chess > maxChess)
			{
				chess[player2ChessOrder[0].Item1, player2ChessOrder[0].Item2] = 0;
				player2ChessOrder.RemoveAt(0);
				player2Chess--;
			}
			order = 1;
		}
	}
	//判断是否可以赢
	public bool aiJudgeWin(int x, int y)
	{
		//预设下棋
		chess[x, y] = 2;
		if (chess[x, 0] == chess[x, 1] && chess[x, 1] == chess[x, 2] && chess[x, 0] == 2)
		{
			chess[x, y] = 0;
			return true;
		}
		if (chess[0, y] == chess[1, y] && chess[1, y] == chess[2, y] && chess[0, y] == 2)
		{
			chess[x, y] = 0;
			return true;
		}
		if (chess[0, 0] == chess[1, 1] && chess[1, 1] == chess[2, 2] && chess[0, 0] == 2)
		{
			chess[x, y] = 0;
			return true;
		}
		if (chess[0, 2] == chess[1, 1] && chess[1, 1] == chess[2, 0] && chess[0, 2] == 2)
		{
			chess[x, y] = 0;
			return true;
		}
		chess[x, y] = 0;
		return false;
	}

	//判断是否可以防守
	public bool aiJudgeDefend(int x, int y)
	{
		//预设下棋
		chess[x, y] = 1;
		if (chess[x, 0] == chess[x, 1] && chess[x, 1] == chess[x, 2] && chess[x, 0] == 1)
		{
			chess[x, y] = 0;
			return true;
		}
		if (chess[0, y] == chess[1, y] && chess[1, y] == chess[2, y] && chess[0, y] == 1)
		{
			chess[x, y] = 0;
			return true;
		}
		if (chess[0, 0] == chess[1, 1] && chess[1, 1] == chess[2, 2] && chess[0, 0] == 1)
		{
			chess[x, y] = 0;
			return true;
		}
		if (chess[0, 2] == chess[1, 1] && chess[1, 1] == chess[2, 0] && chess[0, 2] == 1)
		{
			chess[x, y] = 0;
			return true;
		}
		chess[x, y] = 0;
		return false;
	}
	//判断是否可以形成两个棋子
	public bool aiJudgeTwoChess(int x, int y)
	{
		//预设下棋
		chess[x, y] = 2;
		//判断列
		int twoChessInRank = 0;
		if (chess[x, 0] == 2)
		{
			twoChessInRank++;
		}
		if (chess[x, 1] == 2)
		{
			twoChessInRank++;
		}
		if (chess[x, 2] == 2)
		{
			twoChessInRank++;
		}
		//判断行
		int twoChessInColumn = 0;
		if (chess[0, y] == 2)
		{
			twoChessInColumn++;
		}
		if (chess[1, y] == 2)
		{
			twoChessInColumn++;
		}
		if (chess[2, y] == 2)
		{
			twoChessInColumn++;
		}
		int twoChessInDiagonal = 0;
		int twoChessInAntiDiagonal = 0;
		//判断是否属于对角线位置
		if (x == y)
		{
			//判断对角线
			if (chess[0, 0] == 2)
			{
				twoChessInDiagonal++;
			}
			if (chess[1, 1] == 2)
			{
				twoChessInDiagonal++;
			}
			if (chess[2, 2] == 2)
			{
				twoChessInDiagonal++;
			}
		}
		//判断是否属于反对角线位置
		if (x + y == 2)
		{
			//判断反对角线
			if (chess[0, 2] == 2)
			{
				twoChessInAntiDiagonal++;
			}
			if (chess[1, 1] == 2)
			{
				twoChessInAntiDiagonal++;
			}
			if (chess[2, 0] == 2)
			{
				twoChessInAntiDiagonal++;
			}
		}
		if (twoChessInRank == 2 || twoChessInColumn == 2 || twoChessInDiagonal == 2 || twoChessInAntiDiagonal == 2)
		{
			chess[x, y] = 0;
			return true;
		}
		chess[x, y] = 0;
		return false;
	}

	//判断是否可以形成一个棋子
	public bool aiJudgeOneChess(int x, int y)
	{
		//预设下棋
		chess[x, y] = 2;
		//判断列
		int oneChessInRank = 0;
		if (chess[x, 0] == 2)
		{
			oneChessInRank++;
		}
		if (chess[x, 1] == 2)
		{
			oneChessInRank++;
		}
		if (chess[x, 2] == 2)
		{
			oneChessInRank++;
		}
		//判断行
		int oneChessInColumn = 0;
		if (chess[0, y] == 2)
		{
			oneChessInColumn++;
		}
		if (chess[1, y] == 2)
		{
			oneChessInColumn++;
		}
		if (chess[2, y] == 2)
		{
			oneChessInColumn++;
		}
		int oneChessInDiagonal = 0;
		int oneChessInAntiDiagonal = 0;
		//判断是否属于对角线位置
		if (x == y)
		{
			//判断对角线
			if (chess[0, 0] == 2)
			{
				oneChessInDiagonal++;
			}
			if (chess[1, 1] == 2)
			{
				oneChessInDiagonal++;
			}
			if (chess[2, 2] == 2)
			{
				oneChessInDiagonal++;
			}
		}
		//判断是否属于反对角线位置
		if (x + y == 2)
		{
			//判断反对角线
			if (chess[0, 2] == 2)
			{
				oneChessInAntiDiagonal++;
			}
			if (chess[1, 1] == 2)
			{
				oneChessInAntiDiagonal++;
			}
			if (chess[2, 0] == 2)
			{
				oneChessInAntiDiagonal++;
			}
		}
		if (oneChessInRank == 1 || oneChessInColumn == 1 || oneChessInDiagonal == 1 || oneChessInAntiDiagonal == 1)
		{
			chess[x, y] = 0;
			return true;
		}
		chess[x, y] = 0;
		return false;
	}
	//判断是否可以形成两个空位
	public bool aiJudgeTwoSpace(int x, int y)
	{
		//预设下棋
		chess[x, y] = 2;
		//判断列
		int twoSpaceInRank = 0;
		if (chess[x, 0] == 0)
		{
			twoSpaceInRank++;
		}
		if (chess[x, 1] == 0)
		{
			twoSpaceInRank++;
		}
		if (chess[x, 2] == 0)
		{
			twoSpaceInRank++;
		}
		//判断行
		int twoSpaceInColumn = 0;
		if (chess[0, y] == 0)
		{
			twoSpaceInColumn++;
		}
		if (chess[1, y] == 0)
		{
			twoSpaceInColumn++;
		}
		if (chess[2, y] == 0)
		{
			twoSpaceInColumn++;
		}
		int twoSpaceInDiagonal = 0;
		int twoSpaceInAntiDiagonal = 0;
		//判断是否属于对角线位置
		if (x == y)
		{
			//判断对角线
			if (chess[0, 0] == 0)
			{
				twoSpaceInDiagonal++;
			}
			if (chess[1, 1] == 0)
			{
				twoSpaceInDiagonal++;
			}
			if (chess[2, 2] == 0)
			{
				twoSpaceInDiagonal++;
			}
		}
		//判断是否属于反对角线位置
		if (x + y == 2)
		{
			//判断反对角线
			if (chess[0, 2] == 0)
			{
				twoSpaceInAntiDiagonal++;
			}
			if (chess[1, 1] == 0)
			{
				twoSpaceInAntiDiagonal++;
			}
			if (chess[2, 0] == 0)
			{
				twoSpaceInAntiDiagonal++;
			}
		}
		if (twoSpaceInRank == 2 || twoSpaceInColumn == 2 || twoSpaceInDiagonal == 2 || twoSpaceInAntiDiagonal == 2)
		{
			chess[x, y] = 0;
			return true;
		}
		chess[x, y] = 0;
		return false;
	}
	//判断是否可以形成一个空位
	public bool aiJudgeOneSpace(int x, int y)
	{
		//预设下棋
		chess[x, y] = 2;
		//判断列
		int oneSpaceInRank = 0;
		if (chess[x, 0] == 0)
		{
			oneSpaceInRank++;
		}
		if (chess[x, 1] == 0)
		{
			oneSpaceInRank++;
		}
		if (chess[x, 2] == 0)
		{
			oneSpaceInRank++;
		}
		//判断行
		int oneSpaceInColumn = 0;
		if (chess[0, y] == 0)
		{
			oneSpaceInColumn++;
		}
		if (chess[1, y] == 0)
		{
			oneSpaceInColumn++;
		}
		if (chess[2, y] == 0)
		{
			oneSpaceInColumn++;
		}
		int oneSpaceInDiagonal = 0;
		int oneSpaceInAntiDiagonal = 0;
		//判断是否属于对角线位置
		if (x == y)
		{
			//判断对角线
			if (chess[0, 0] == 0)
			{
				oneSpaceInDiagonal++;
			}
			if (chess[1, 1] == 0)
			{
				oneSpaceInDiagonal++;
			}
			if (chess[2, 2] == 0)
			{
				oneSpaceInDiagonal++;
			}
		}
		//判断是否属于反对角线位置
		if (x + y == 2)
		{
			//判断反对角线
			if (chess[0, 2] == 0)
			{
				oneSpaceInAntiDiagonal++;
			}
			if (chess[1, 1] == 0)
			{
				oneSpaceInAntiDiagonal++;
			}
			if (chess[2, 0] == 0)
			{
				oneSpaceInAntiDiagonal++;
			}
		}
		if (oneSpaceInRank == 1 || oneSpaceInColumn == 1 || oneSpaceInDiagonal == 1 || oneSpaceInAntiDiagonal == 1)
		{
			chess[x, y] = 0;
			return true;
		}
		chess[x, y] = 0;
		return false;
	}

	//判断是否到达最大棋子数
	public void judgeMaxChess()
	{

		if (isGameOver)
		{
			for (int i = 0; i < 3; i++)
			{
				for (int j = 0; j < 3; j++)
				{
					if (chess[i, j] == 3)
					{
						chess[i, j] = 1;
					}
					if (chess[i, j] == 4)
					{
						chess[i, j] = 2;
					}
				}
			}
		}
		else
		{
			if (player1Chess == maxChess && order == 1)
			{
				chess[player1ChessOrder[0].Item1, player1ChessOrder[0].Item2] = 3;
			}
			if (player2Chess == maxChess && order == 2)
			{
				chess[player2ChessOrder[0].Item1, player2ChessOrder[0].Item2] = 4;
			}
		}
	}

	//改变玩家人数
	public void changePlayerNumber()
	{
		if (gameStatusNum == 0)
		{
			if (playerNum == 1)
			{
				playerNum = 2;
				chgPlayerNum.Text = "2P";
			}
			else
			{
				playerNum = 1;
				chgPlayerNum.Text = "1P";
			}
		}
	}

	//更新棋盘上的棋子
	public void updateChess()
	{
		switch (chess[0, 0])
		{
			case 0:
				Button1.TextureNormal = spaceTexture;
				break;
			case 1:
				Button1.TextureNormal = player1Texture;
				break;
			case 2:
				Button1.TextureNormal = player2Texture;
				break;
			case 3:
				Button1.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button1.TextureNormal = plyer2disTexture;
				break;
		}
		switch (chess[0, 1])
		{
			case 0:
				Button2.TextureNormal = spaceTexture;
				break;
			case 1:
				Button2.TextureNormal = player1Texture;
				break;
			case 2:
				Button2.TextureNormal = player2Texture;
				break;
			case 3:
				Button2.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button2.TextureNormal = plyer2disTexture;
				break;
		}
		switch (chess[0, 2])
		{
			case 0:
				Button3.TextureNormal = spaceTexture;
				break;
			case 1:
				Button3.TextureNormal = player1Texture;
				break;
			case 2:
				Button3.TextureNormal = player2Texture;
				break;
			case 3:
				Button3.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button3.TextureNormal = plyer2disTexture;
				break;
		}
		switch (chess[1, 0])
		{
			case 0:
				Button4.TextureNormal = spaceTexture;
				break;
			case 1:
				Button4.TextureNormal = player1Texture;
				break;
			case 2:
				Button4.TextureNormal = player2Texture;
				break;
			case 3:
				Button4.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button4.TextureNormal = plyer2disTexture;
				break;

		}
		switch (chess[1, 1])
		{
			case 0:
				Button5.TextureNormal = spaceTexture;
				break;
			case 1:
				Button5.TextureNormal = player1Texture;
				break;
			case 2:
				Button5.TextureNormal = player2Texture;
				break;
			case 3:
				Button5.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button5.TextureNormal = plyer2disTexture;
				break;
		}
		switch (chess[1, 2])
		{
			case 0:
				Button6.TextureNormal = spaceTexture;
				break;
			case 1:
				Button6.TextureNormal = player1Texture;
				break;
			case 2:
				Button6.TextureNormal = player2Texture;
				break;
			case 3:
				Button6.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button6.TextureNormal = plyer2disTexture;
				break;
		}
		switch (chess[2, 0])
		{
			case 0:
				Button7.TextureNormal = spaceTexture;
				break;
			case 1:
				Button7.TextureNormal = player1Texture;
				break;
			case 2:
				Button7.TextureNormal = player2Texture;
				break;
			case 3:
				Button7.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button7.TextureNormal = plyer2disTexture;
				break;
		}
		switch (chess[2, 1])
		{
			case 0:
				Button8.TextureNormal = spaceTexture;
				break;
			case 1:
				Button8.TextureNormal = player1Texture;
				break;
			case 2:
				Button8.TextureNormal = player2Texture;
				break;
			case 3:
				Button8.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button8.TextureNormal = plyer2disTexture;
				break;
		}
		switch (chess[2, 2])
		{
			case 0:
				Button9.TextureNormal = spaceTexture;
				break;
			case 1:
				Button9.TextureNormal = player1Texture;
				break;
			case 2:
				Button9.TextureNormal = player2Texture;
				break;
			case 3:
				Button9.TextureNormal = plyer1disTexture;
				break;
			case 4:
				Button9.TextureNormal = plyer2disTexture;
				break;
		}
	}

	public void _on_Button1_pressed()
	{
		GD.Print("Button1 pressed: ");
		playChess(0, 0);
	}

	public void _on_Button2_pressed()
	{
		GD.Print("Button2 pressed: ");
		playChess(0, 1);
	}

	public void _on_Button3_pressed()
	{
		GD.Print("Button3 pressed: ");
		playChess(0, 2);
	}

	public void _on_Button4_pressed()
	{
		GD.Print("Button4 pressed: ");
		playChess(1, 0);
	}

	public void _on_Button5_pressed()
	{
		GD.Print("Button5 pressed: ");
		playChess(1, 1);
	}

	public void _on_Button6_pressed()
	{
		GD.Print("Button6 pressed: ");
		playChess(1, 2);
	}

	public void _on_Button7_pressed()
	{
		GD.Print("Button7 pressed: ");
		playChess(2, 0);
	}

	public void _on_Button8_pressed()
	{
		GD.Print("Button8 pressed: ");
		playChess(2, 1);
	}

	public void _on_Button9_pressed()
	{
		GD.Print("Button9 pressed: ");
		playChess(2, 2);
	}

}
