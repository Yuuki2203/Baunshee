#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine;

[Tooltip("エディタで設定されたプリセットを用いて、カメラ位置を水平・垂直軸に沿ってシェイクします")]
public class PC2DShakeWithPreset : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("ProCamera2D コンポーネントを持つカメラ（通常は MainCamera）")]
	public FsmGameObject MainCamera;

[Tooltip("エディタで設定されたシェイク・プリセット名")]
	public FsmString PresetName;

	public override void OnEnter()
	{
		var shake = MainCamera.Value.GetComponent<ProCamera2DShake>();

		if (shake == null)
			Debug.LogError("The ProCamera2D component needs to have the Shake plugin enabled.");

		if (ProCamera2D.Instance != null && shake != null)
			shake.Shake(PresetName.Value);

		Finish();
	}
}
#endif