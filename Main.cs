using Godot;
using System;
using System.Collections.Generic;

public partial class Main : Control
{
	private Button _refreshButton;
	private Button _daughterButton;
	private TextureRect[] _cardRects = new TextureRect[4];

	// 可替换成你自己的卡牌资源目录(在Godot中一般放在 res://image/cards/... )
	private List<string> _allCardPaths = new List<string>();
	private List<string> _daughterCardPaths = new List<string>();

	public override void _Ready()
	{
		// 获取按钮
		_refreshButton = GetNode<Button>("RefreshButton");
		_daughterButton = GetNode<Button>("DaughterButton");

		// 获取四个展示卡牌的 TextureRect
		_cardRects[0] = GetNode<TextureRect>("Card0");
		_cardRects[1] = GetNode<TextureRect>("Card1");
		_cardRects[2] = GetNode<TextureRect>("Card2");
		_cardRects[3] = GetNode<TextureRect>("Card3");

		// 初始化所有卡牌资源路径(举例，这里要把卡牌图片放进 Godot 的 res://image/cards/ 文件夹)
		// 你可以根据你拥有的卡数量循环添加路径
		for (int i = 1; i <= 54; i++)
		{
			_allCardPaths.Add($"res://image/cards/{i}.png");
		}

		// Daughter牌组(示例：3,6,11,23,26,29,50). 你可以改成任何想要的索引
		_daughterCardPaths.Add("res://image/cards/3.png");
		_daughterCardPaths.Add("res://image/cards/6.png");
		_daughterCardPaths.Add("res://image/cards/11.png");
		_daughterCardPaths.Add("res://image/cards/23.png");
		_daughterCardPaths.Add("res://image/cards/26.png");
		_daughterCardPaths.Add("res://image/cards/29.png");
		_daughterCardPaths.Add("res://image/cards/50.png");

		// 绑定按钮事件
		_refreshButton.Pressed += OnRefreshPressed;
		_daughterButton.Pressed += OnDaughterPressed;

		// 初始化默认的卡背面(如果你有卡背图片的话)
		var defaultCard = GD.Load<Texture2D>("res://image/cards/b1fv.png");
		foreach (var rect in _cardRects)
		{
			rect.Texture = defaultCard;
		}
	}

	private void OnRefreshPressed()
	{
		// 乱序洗牌
		Shuffle(_allCardPaths);
		// 前四张赋给 TextureRect
		for (int i = 0; i < 4; i++)
		{
			var texture = GD.Load<Texture2D>(_allCardPaths[i]);
			_cardRects[i].Texture = texture;
		}
	}

	private void OnDaughterPressed()
	{
		// 乱序洗牌
		Shuffle(_daughterCardPaths);
		// 前四张赋给 TextureRect
		for (int i = 0; i < 4; i++)
		{
			var texture = GD.Load<Texture2D>(_daughterCardPaths[i]);
			_cardRects[i].Texture = texture;
		}
	}

	private void Shuffle<T>(List<T> list)
	{
		// Fisher-Yates Shuffle
		Random random = new Random();
		int n = list.Count;
		while (n > 1)
		{
			n--;
			int k = random.Next(n + 1);
			T temp = list[k];
			list[k] = list[n];
			list[n] = temp;
		}
	}
}
