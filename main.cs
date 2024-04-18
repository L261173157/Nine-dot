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

	//重置游戏
	public void resetGame()
	{
		initChess();
		isGameOver = false;
		winPlayer = 0;
		order = 1;
		player1Chess = 0;
		player2Chess = 0;
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
			if (winPlayer == 1)
			{
				gameStatus.Text = "玩家1胜利";
			}
			else if (winPlayer == 2)
			{
				gameStatus.Text = "玩家2胜利";
			}
			else
			{
				gameStatus.Text = "平局";
			}
		}
		else
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

		if (isGameOver)
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
				order = 2;
			}
			else
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
		judgeGameOver();
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
			if (player1Chess == maxChess&&order==1)
			{
				chess[player1ChessOrder[0].Item1, player1ChessOrder[0].Item2] = 3;
			}
			if (player2Chess == maxChess&&order==2)
			{
				chess[player2ChessOrder[0].Item1, player2ChessOrder[0].Item2] = 4;
			}
		}
	}

	//改变玩家人数
	public void changePlayerNumber()
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
