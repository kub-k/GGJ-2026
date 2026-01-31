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

    private bool _isHeaven;

    [Header("Cehennem Süresi")]
    public float maskDuration = 10f; // toplam süre (saniye)
    private float currentMaskTime;
    private bool maskLocked = false; // süre bitince true olacak

    void Start()
    {
        _isHeaven = true;
        // Başlangıçta tüm Tilemap'leri hafızaya al (Performans için önemli)
        tilemapsHeaven = gridHeaven.GetComponentsInChildren<Tilemap>();
        tilemapsHell = gridHell.GetComponentsInChildren<Tilemap>();

        // İlk dünya durumunu ayarla
        UpdateDimensionVisuals();

        currentMaskTime = maskDuration; 
    }

    void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            // maske kilitliyse ve cehenneme geçilmeye çalışılıyorsa engelle
            if (maskLocked && _isHeaven)
            {
                Debug.Log("Maske süresi bitti! Cehenneme geçilemez.");
                return;
            }
            _isHeaven = !_isHeaven;
            UpdateDimensionVisuals();
        }

        if (!_isHeaven && !maskLocked && currentMaskTime > 0f)
        {
            currentMaskTime -= Time.deltaTime;
            Debug.Log("Maske süresi kalan: " + currentMaskTime.ToString("F2"));

            if (currentMaskTime <= 0f)
            {
                currentMaskTime = 0f;
                maskLocked = true;

                Debug.Log("Maske süresi bitti! Cehennem artık kilitli.");

                // Oyuncuyu zorla cennete geri al
                _isHeaven = true;
                UpdateDimensionVisuals();
            }
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

