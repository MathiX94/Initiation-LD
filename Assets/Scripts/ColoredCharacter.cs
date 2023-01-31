using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColoredCharacter : MonoBehaviour
{
    [SerializeField] public ElementColor color;

    public void OnValidate()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer && WorldParameters.Instance)
            renderer.color = WorldParameters.Instance.GetColor(color);

        EditorApplication.delayCall += () =>
        {
            if (this != null)
                gameObject.layer = WorldParameters.Instance.GetColorCharacterMask(color);

			CharacterController2D controller = GetComponent<CharacterController2D>();
			if (controller)
				controller.groundCheck.gameObject.layer = WorldParameters.Instance.GetColorCharacterGroundMask(color);
		};

       
    }
}
