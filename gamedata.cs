using Godot;
using System;

public partial class gamedata : Node
{
	//全局共享数据
	//是否结束
	public bool isGameOver = false;
	//玩家1或者2是否胜利，1为玩家1,2为玩家2
	public int winPlayer = 0;
	//玩家人数
	public int playerNum = 1;

	public override void _Ready()
	{
	}

	
}
