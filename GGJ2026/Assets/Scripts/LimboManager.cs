using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap kullanıyorsanız bu kütüphane şart
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class LimboManager : MonoBehaviour
{
    [Header("Fiziksel Colliderlar")]
    public GameObject bodyColliderHeaven;
    public GameObject feetColliderHeaven;
    public GameObject bodyColliderHell;
    public GameObject feetColliderHell;
    
    [Header("Grid Referansları")]
    public GameObject gridHeaven; // Dünya A'nın ana Grid objesi
    public GameObject gridHell; // Dünya B'nın ana Grid objesi
    
    private Tilemap[] tilemapsHeaven;
    private Tilemap[] tilemapsHell;
    
    [Header("Görsel Ayarlar")]
    [Range(0, 1)] public float activeAlpha = 1f;    // Aktif dünyanın netliği
    [Range(0, 1)] public float inactiveAlpha = 0f; // Pasif dünyanın şeffaflığı

    [Header("Oyuncu Ayarları")]
    public Animator animator;
    public RuntimeAnimatorController baseController;
    public AnimatorOverrideController overrideHell;

    [Header("Arkaplanlar")]
    public GameObject heavenBackground;
    public GameObject hellBackground;

    bool _isHeaven = true;

    void Start()
    {
        // Başlangıçta tüm Tilemap'leri hafızaya al (Performans için önemli)
        tilemapsHeaven = gridHeaven.GetComponentsInChildren<Tilemap>();
        tilemapsHell = gridHell.GetComponentsInChildren<Tilemap>();

        // İlk dünya durumunu ayarla
        UpdateDimensionVisuals();
    }
    
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            _isHeaven = !_isHeaven;
            UpdateDimensionVisuals();
        }
    }

    void UpdateDimensionVisuals()
    {
        bodyColliderHeaven.SetActive(_isHeaven);
        feetColliderHeaven.SetActive(_isHeaven);
        
        bodyColliderHell.SetActive(!_isHeaven);
        feetColliderHell.SetActive(!_isHeaven);
        
        // Dünya A'yı güncelle
        SetAlphaForGroup(tilemapsHeaven, _isHeaven ? activeAlpha : inactiveAlpha);
        
        // Dünya B'yi güncelle
        SetAlphaForGroup(tilemapsHell, _isHeaven ? inactiveAlpha : activeAlpha);

        // Animasyon kontrolcüsünü değiştir
        animator.runtimeAnimatorController = _isHeaven ? baseController : overrideHell;
        
        // isHeaven a göre background game object set active
        heavenBackground.SetActive(_isHeaven);
        hellBackground.SetActive(!_isHeaven);
    }

    void SetAlphaForGroup(Tilemap[] maps, float alpha)
    {
        foreach (Tilemap tm in maps)
        {
            Color c = tm.color;
            c.a = alpha;
            tm.color = c;
        }
    }
}
