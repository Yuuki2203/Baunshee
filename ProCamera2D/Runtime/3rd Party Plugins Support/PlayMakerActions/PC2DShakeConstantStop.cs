#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine;

[Tooltip("カメラの現在の定常シェイクを停止します")]
public class PC2DShakeConstantStop : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("ProCamera2D コンポーネントを持つカメラ（通常は MainCamera）")]
	public FsmGameObject MainCamera;

	public override void OnEnter()
	{
		var shake = MainCamera.Value.GetComponent<ProCamera2DShake>();

		if (shake == null)
			Debug.LogError("The ProCamera2D component needs to have the Shake plugin enabled.");

		if (ProCamera2D.Instance != null && shake != null)
			shake.StopConstantShaking();

		Finish();
	}
}
#endif