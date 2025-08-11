#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine;

[Tooltip("指定値に基づき、カメラ位置を水平・垂直軸に沿ってシェイクします")]
public class PC2DShakeWithValues : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("ProCamera2D コンポーネントを持つカメラ（通常は MainCamera）")]
	public FsmGameObject MainCamera;

[Tooltip("各軸におけるシェイク強度")]
	public FsmVector2 Strength;

[Tooltip("シェイクの継続時間")]
	public FsmFloat Duration = 1;

[Tooltip("シェイクの振動量。1 未満や 100 超の値は推奨されません")]
	public FsmInt Vibrato = 10;

[Tooltip("シェイクのランダム性の度合い")]
	[HasFloatSlider(0, 1)]
	public FsmFloat Randomness = .1f;

[Tooltip("シェイクの初期角度。ランダムにする場合は -1 を使用")]
	[HasFloatSlider(-1, 360)]
	public FsmInt InitialAngle = 10;

[Tooltip("シェイク中にカメラが到達しうる最大回転")]
	public FsmVector3 Rotation;

[Tooltip("シェイクのスムーズさ。0 で即時")]
	[HasFloatSlider(0, .5f)]
	public FsmFloat Smoothness;

	public override void OnEnter()
	{
		var shake = MainCamera.Value.GetComponent<ProCamera2DShake>();

		if (shake == null)
			Debug.LogError("The ProCamera2D component needs to have the Shake plugin enabled.");

		if (ProCamera2D.Instance != null && shake != null)
			shake.Shake(
				Duration.Value,
				Strength.Value,
				Vibrato.Value,
				Randomness.Value,
				InitialAngle.Value,
				Rotation.Value,
				Smoothness.Value);

		Finish();
	}
}
#endif