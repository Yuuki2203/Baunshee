/******************************************************************************
 * Spine Runtimes License Agreement
 * Last updated April 5, 2025. Replaces all prior versions.
 *
 * Copyright (c) 2013-2025, Esoteric Software LLC
 *
 * Integration of the Spine Runtimes into software or otherwise creating
 * derivative works of the Spine Runtimes is permitted under the terms and
 * conditions of Section 2 of the Spine Editor License Agreement:
 * http://esotericsoftware.com/spine-editor-license
 *
 * Otherwise, it is permitted to integrate the Spine Runtimes into software
 * or otherwise create derivative works of the Spine Runtimes (collectively,
 * "Products"), provided that each user of the Products must obtain their own
 * Spine Editor license and redistribution of the Products in any form must
 * include this license and copyright notice.
 *
 * THE SPINE RUNTIMES ARE PROVIDED BY ESOTERIC SOFTWARE LLC "AS IS" AND ANY
 * EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A PARTICULAR PURPOSE ARE
 * DISCLAIMED. IN NO EVENT SHALL ESOTERIC SOFTWARE LLC BE LIABLE FOR ANY
 * DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR CONSEQUENTIAL DAMAGES
 * (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES,
 * BUSINESS INTERRUPTION, OR LOSS OF USE, DATA, OR PROFITS) HOWEVER CAUSED AND
 * ON ANY THEORY OF LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF
 * THE SPINE RUNTIMES, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
 *****************************************************************************/

using UnityEngine;

namespace Spine.Unity {

	// To use this example component, add it to your SkeletonAnimation Spine GameObject.
	// This component will disable that SkeletonAnimation component to prevent it from calling its own Update and LateUpdate methods.

	[DisallowMultipleComponent]
	public sealed class SkeletonAnimationFixedTimestep : MonoBehaviour {
		#region Inspector
		public SkeletonAnimation skeletonAnimation;

		[Tooltip("各フレームの秒数。12fps の場合は Unity インスペクターで '1/12' と入力してください。")]
		public float frameDeltaTime = 1 / 15f;

		[Header("詳細設定")]
		[Tooltip("固定タイムステップの最大回数。フレームレートが制限値を下回る場合に上限を設けます。フレームレートが常に十分速い場合は何もしません。")]
		public int maxFrameSkip = 4;

		[Tooltip("有効にすると、アニメーションとスケルトンが更新されるフレームと同じフレームでのみメッシュを更新します。別の固定タイムステップ外でスケルトンを変更する場合は無効にするか、手動で SkeletonAnimation.LateUpdate を呼び出してください。")]
		public bool frameskipMeshUpdate = true;

		[Tooltip("内部アキュムレータの初期値。複数スケルトンの更新タイミングをずらしたい場合は、フレームのデルタタイムの一部を設定してください。")]
		public float timeOffset;
		#endregion

		float accumulatedTime = 0;
		bool requiresNewMesh;

		void OnValidate () {
			skeletonAnimation = GetComponent<SkeletonAnimation>();
			if (frameDeltaTime <= 0) frameDeltaTime = 1 / 60f;
			if (maxFrameSkip < 1) maxFrameSkip = 1;
		}

		void Awake () {
			requiresNewMesh = true;
			accumulatedTime = timeOffset;
		}

		void Update () {
			if (skeletonAnimation.enabled)
				skeletonAnimation.enabled = false;

			accumulatedTime += Time.deltaTime;

			float frames = 0;
			while (accumulatedTime >= frameDeltaTime) {
				frames++;
				if (frames > maxFrameSkip) break;
				accumulatedTime -= frameDeltaTime;
			}

			if (frames > 0) {
				skeletonAnimation.Update(frames * frameDeltaTime);
				requiresNewMesh = true;
			}
		}

		void LateUpdate () {
			if (frameskipMeshUpdate && !requiresNewMesh) return;

			skeletonAnimation.LateUpdate();
			requiresNewMesh = false;
		}
	}
}
