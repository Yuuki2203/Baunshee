#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine;

[Tooltip("シネマティックを開始または停止します")]
public class PC2DCinematicsToggle : FsmStateActionProCamera2DBase
{
	[RequiredField]
[Tooltip("ProCamera2DCinematics コンポーネントを含む GameObject")]
	public FsmGameObject Cinematics;

	public override void OnEnter()
	{
		var cinematics = Cinematics.Value.GetComponent<ProCamera2DCinematics>();

		if (cinematics == null)
			Debug.LogError("No Cinematics component found in the gameObject: " + Cinematics.Value.name);

		if (ProCamera2D.Instance != null && cinematics != null)
			cinematics.Toggle();

		Finish();
	}
}
#endif