﻿using System.Linq;
using UnityEngine;

namespace KorzUtils.Helper;

/// <summary>
/// Stores various references from the HeroController.
/// </summary>
public static class HeroHelper
{
    #region Members

    private static Rigidbody2D _rigidBody;

    private static GameObject[] _nailAttacks = new GameObject[5];

    private static GameObject[] _nailArts = new GameObject[3];

    private static BoxCollider2D _collider;

    private static tk2dSprite[] _dreamNailSprites = new tk2dSprite[4];

    private static tk2dSprite _sprite;

    #endregion

    #region Constructors

    static HeroHelper()
    {
        On.HeroController.Start += HeroController_Start;
    }

    #endregion

    #region Properties

    /// <summary>
    /// Gets the rigidbody of the hero.
    /// </summary>
    public static Rigidbody2D RigidBody => _rigidBody == null ? _rigidBody = HeroController.instance?.gameObject?.GetComponent<Rigidbody2D>() : _rigidBody;

    /// <summary>
    /// Gets the normal nail attack objects.
    /// </summary>
    public static GameObject[] NailAttacks
    {
        get
        {
            if (_nailAttacks.Any(x => x == null))
            {
                GameObject attackDirections = GameObject.Find("Knight/Attacks");
                _nailAttacks[0] = attackDirections?.transform?.Find("Slash")?.gameObject;
                _nailAttacks[1] = attackDirections?.transform?.Find("AltSlash")?.gameObject;
                _nailAttacks[2] = attackDirections?.transform?.Find("UpSlash")?.gameObject;
                _nailAttacks[3] = attackDirections?.transform?.Find("DownSlash")?.gameObject;
                _nailAttacks[4] = attackDirections?.transform?.Find("WallSlash")?.gameObject;
            }
            return _nailAttacks;
        }
    }

    /// <summary>
    /// Gets the nail art objects.
    /// </summary>
    public static GameObject[] NailArts
    {
        get
        {
            if (_nailAttacks.Any(x => x == null))
            {
                GameObject attacks = GameObject.Find("Knight/Attacks");
                _nailArts[0] = attacks?.transform?.Find("Great Slash")?.gameObject;
                _nailArts[1] = attacks?.transform?.Find("Dash Slash")?.gameObject;
                _nailArts[2] = attacks?.transform?.Find("Cyclone Slash")?.gameObject;
            }
            return _nailArts;
        }
    }

    /// <summary>
    /// Gets the collider of the hero.
    /// </summary>
    public static BoxCollider2D Collider => _collider == null && HeroController.instance != null ? _collider = HeroController.instance.GetComponent<BoxCollider2D>() : _collider;

    public static tk2dSprite[] DreamNailSprites
    {
        get
        {
            if (_dreamNailSprites == null || _dreamNailSprites.Any(x => x == null))
                _dreamNailSprites = GameObject.Find("Knight/Dream Effects")?.GetComponentsInChildren<tk2dSprite>(true);
            return _dreamNailSprites;
        }
    }

    public static tk2dSprite Sprite => _sprite == null && HeroController.instance != null ? _sprite = HeroController.instance.GetComponent<tk2dSprite>() : _sprite;

    #endregion

    #region Eventhandler

    private static void HeroController_Start(On.HeroController.orig_Start orig, HeroController self)
    {
        orig(self);
        // Reset all references. They will be assigned again once required.
        _sprite = null;
        _collider = null;
        _rigidBody = null;
        for (int i = 0; i < _nailArts.Length; i++)
            _nailArts[i] = null;
        for (int i = 0; i < _nailAttacks.Length; i++)
            _nailAttacks[i] = null;
        for (int i = 0; i < _dreamNailSprites.Length; i++)
            _dreamNailSprites[i] = null;
    }

    #endregion
}
