#if PLAYMAKER
using Com.LuisPedroFonseca.ProCamera2D;
using HutongGames.PlayMaker;
using TooltipAttribute = HutongGames.PlayMaker.TooltipAttribute;
using UnityEngine;

[Tooltip("Rooms 拡張使用時にルームへ入室します")]
public class PC2DRoomsEnter : FsmStateActionProCamera2DBase
{
[Tooltip("インデックスで現在のルームを設定"), RequiredField]
	public FsmInt RoomIndex;
[Tooltip("ID で現在のルームを設定（ID 指定はインデックス指定より優先）")]
	public FsmString RoomId;
[Tooltip("false の場合は即座に遷移、true の場合は Rooms 拡張エディタで設定したトランジションを使用します。")]
	public bool UseTransition = true;
	ProCamera2DRooms _rooms;

	public override void Reset()
	{
		RoomIndex = 0;
		RoomId = null;
	}

	public override void OnEnter()
	{
		var pc2d = ProCamera2D.Instance;
		if (pc2d == null)
		{
			Debug.LogError("No ProCamera2D found! Please add the core component to your Main Camera.");
			Finish();
			return;
		}

		_rooms = pc2d.GetComponent<ProCamera2DRooms>();
		if (_rooms == null)
		{
			Debug.LogError("No Rooms extension found in ProCamera2D!");
			Finish();
			return;
		}

		SetRoom();
		Finish();
	}

	void SetRoom()
	{
		if (!RoomId.IsNone && !string.IsNullOrEmpty(RoomId.Value))
		{
			_rooms.EnterRoom(RoomId.Value, UseTransition);
		}
		else
		{
			_rooms.EnterRoom(RoomIndex.Value, UseTransition);
		}
	}
}
#endif