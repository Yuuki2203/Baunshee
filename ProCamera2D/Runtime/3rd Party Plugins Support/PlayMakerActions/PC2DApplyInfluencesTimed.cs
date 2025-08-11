#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine;

[Tooltip("指定した複数の影響を、それぞれの時間でカメラに適用します")]
public class PC2DApplyInfluencesTimed : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("適用する影響を表すベクトルの配列")]
	public FsmVector2[] Influences;

	[RequiredField]
[Tooltip("各影響に対応する適用時間の配列")]
	public FsmFloat[] Durations;

	public override void Reset()
	{
		Influences = new FsmVector2[0];
		Durations = new FsmFloat[0];
	}

	public override void OnEnter()
	{
		if (ProCamera2D.Instance != null)
		{
			var entries = Influences.GetLength(0);

			var influences = new Vector2[entries];
			for (int i = 0; i < entries; i++)
			{
				influences[i] = (Influences.GetValue(i) as FsmVector2).Value;
			}

			var durations = new float[entries];
			for (int i = 0; i < entries; i++)
			{
				durations[i] = (Durations.GetValue(i) as FsmFloat).Value;
			}

			ProCamera2D.Instance.ApplyInfluencesTimed(influences, durations);
		}

		Finish();
	}
}
#endif