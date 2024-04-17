using Godot;
using Microsoft.VisualBasic;
using System;
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
	//玩家1图片
	Texture2D player1Texture;
	//玩家2图片
	Texture2D player2Texture;
	//空白图片
	Texture2D spaceTexture;

	//二维数组,用于存储棋盘上的棋子,0为空,1为玩家1,2为玩家2
	int[,] chess = new int[3, 3];

	//下棋次序，1为玩家1,2为玩家2
	int order = 1;
	//玩家最多棋子数
	int maxChess = 3;
	//是否结束
	bool isGameOver = false;
	//玩家1或者2是否胜利，1为玩家1,2为玩家2
	int winPlayer = 0;
	//玩家人数
	int playerNum = 1;

	public override void _Ready()
	{
		GD.Print("Hello, World!");
		Button1 = GetNode<TextureButton>("Control/GridContainer/Button1");
		Button2 = GetNode<TextureButton>("Control/GridContainer/Button2");
		Button3 = GetNode<TextureButton>("Control/GridContainer/Button3");
		Button4 = GetNode<TextureButton>("Control/GridContainer/Button4");
		Button5 = GetNode<TextureButton>("Control/GridContainer/Button5");
		Button6 = GetNode<TextureButton>("Control/GridContainer/Button6");
		Button7 = GetNode<TextureButton>("Control/GridContainer/Button7");
		Button8 = GetNode<TextureButton>("Control/GridContainer/Button8");
		Button9 = GetNode<TextureButton>("Control/GridContainer/Button9");
		player1Texture = GD.Load<Texture2D>("res://asset/player1.png");
		player2Texture = GD.Load<Texture2D>("res://asset/player2.png");
		spaceTexture = GD.Load<Texture2D>("res://asset/space.png");
		initChess();
	}

	public override void _Process(double delta)
	{

		updateChess();
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

		//TODO 未完成，需要继续完善
		if (chess[x, y] == 0)
		{
			chess[x, y] = order;
			if (order == 1)
			{
				order = 2;
			}
			else
			{
				order = 1;
			}
		}
		judgeGameOver();
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
		}
	}

	public void _on_Button1_pressed()
	{
		GD.Print("Button1 pressed: ");
	}

	public void _on_Button2_pressed()
	{
		GD.Print("Button2 pressed: ");

	}

	public void _on_Button3_pressed()
	{
		GD.Print("Button3 pressed: ");

	}

	public void _on_Button4_pressed()
	{
		GD.Print("Button4 pressed: ");
	}

	public void _on_Button5_pressed()
	{
		GD.Print("Button5 pressed: ");
	}

	public void _on_Button6_pressed()
	{
		GD.Print("Button6 pressed: ");
	}

	public void _on_Button7_pressed()
	{
		GD.Print("Button7 pressed: ");
	}

	public void _on_Button8_pressed()
	{
		GD.Print("Button8 pressed: ");
	}

	public void _on_Button9_pressed()
	{
		GD.Print("Button9 pressed: ");
	}

}
