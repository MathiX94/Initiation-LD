using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ColoredElement : MonoBehaviour
{
    [SerializeField] private ElementColor color;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        ColoredCharacter character = collision.gameObject.GetComponent<ColoredCharacter>();
        CharacterController2D controller = collision.gameObject.GetComponent<CharacterController2D>();
        if (character && controller && character.color == color)
        {
            controller.Die();
        }
    }
    public void OnValidate()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if(renderer && WorldParameters.Instance)
            renderer.color = WorldParameters.Instance.GetColor(color);

        EditorApplication.delayCall += () =>
        {
            if (this != null)
                gameObject.layer = WorldParameters.Instance.GetColorGroundMask(color);
        };
        
    }
}
