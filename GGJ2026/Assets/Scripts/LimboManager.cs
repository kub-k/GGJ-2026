using UnityEngine;
using UnityEngine.Tilemaps; // Tilemap kullanıyorsanız bu kütüphane şart
using UnityEngine.InputSystem;

public class LimboManager : MonoBehaviour
{
    [Header("Fiziksel Colliderlar")]
    public GameObject bodyColliderA;
    public GameObject feetColliderA;
    public GameObject bodyColliderB;
    public GameObject feetColliderB;
    
    [Header("Grid Referansları")]
    public GameObject gridA; // Dünya A'nın ana Grid objesi
    public GameObject gridB; // Dünya B'nın ana Grid objesi
    
    private Tilemap[] tilemapsA;
    private Tilemap[] tilemapsB;
    
    
    [Header("Görsel Ayarlar")]
    [Range(0, 1)] public float activeAlpha = 1f;    // Aktif dünyanın netliği
    [Range(0, 1)] public float inactiveAlpha = 0.2f; // Pasif dünyanın şeffaflığı

    [Header("Oyuncu Ayarları")]
    public Animator animator;
    public RuntimeAnimatorController baseController;
    public AnimatorOverrideController overrideB;

    private bool isWorldA = true;

    void Start()
    {
        // Başlangıçta tüm Tilemap'leri hafızaya al (Performans için önemli)
        tilemapsA = gridA.GetComponentsInChildren<Tilemap>();
        tilemapsB = gridB.GetComponentsInChildren<Tilemap>();

        // İlk dünya durumunu ayarla
        UpdateDimensionVisuals();
    }
    
    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            isWorldA = !isWorldA;
            UpdateDimensionVisuals();
        }
    }

    void UpdateDimensionVisuals()
    {
        bodyColliderA.SetActive(isWorldA);
        feetColliderA.SetActive(isWorldA);
        
        bodyColliderB.SetActive(!isWorldA);
        feetColliderB.SetActive(!isWorldA);
        
        // Dünya A'yı güncelle
        SetAlphaForGroup(tilemapsA, isWorldA ? activeAlpha : inactiveAlpha);
        
        // Dünya B'yi güncelle
        SetAlphaForGroup(tilemapsB, isWorldA ? inactiveAlpha : activeAlpha);

        // Animasyon kontrolcüsünü değiştir
        animator.runtimeAnimatorController = isWorldA ? baseController : overrideB;
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
