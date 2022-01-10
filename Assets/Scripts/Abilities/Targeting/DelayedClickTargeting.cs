using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using RPG.Control;

namespace RPG.Abilities.targeting
{
    [CreateAssetMenu(menuName = "Abilities/Targeting/Delayed Click")]
    public class DelayedClickTargeting : TargetingStrategy
    {
        [SerializeField] Texture2D cursorTexture;
        [SerializeField] Vector2 cursorHotspot;

        public override void StartTargeting(GameObject user)
        {
            PlayerController playerController = user.GetComponent<PlayerController>();
            playerController.StartCoroutine(Targeting(user, playerController));
        }

        private IEnumerator Targeting(GameObject user, PlayerController playerController)
        {
            playerController.enabled = false;
            while (true)
            {
                Cursor.SetCursor(cursorTexture, cursorHotspot, CursorMode.Auto);

                if (Input.GetMouseButtonDown(0))
                {
                    //Absorbs the whole mouse click
                    yield return new WaitWhile(() => Input.GetMouseButton(0));

                    playerController.enabled = true;
                    yield break;
                }
                yield return null;
            }
        }
    }
}
