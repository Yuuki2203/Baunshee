#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine;

[Tooltip("エディタで設定されたプリセットを使用して、カメラに一定のシェイクを有効化します")]
public class PC2DShakeConstantWithPreset : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("ProCamera2D コンポーネントを持つカメラ（通常は MainCamera）")]
	public FsmGameObject MainCamera;

[Tooltip("エディタで設定された定常シェイク・プリセット名")]
	public FsmString PresetName;

	public override void OnEnter()
	{
		var shake = MainCamera.Value.GetComponent<ProCamera2DShake>();

		if (shake == null)
			Debug.LogError("The ProCamera2D component needs to have the Shake plugin enabled.");

		if (ProCamera2D.Instance != null && shake != null)
			shake.ConstantShake(PresetName.Value);

		Finish();
	}
}
#endif