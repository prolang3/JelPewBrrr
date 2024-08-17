using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "Scriptable Object/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public UnityEvent OnHit;

    public GameObject Bullet;
    public Sprite Sprite;

    public string Name = "Sample Text";
    public int BaseDamage = 1;
    public float UseDelay = 0.3f;
    public AudioClip FireSound;
    [SerializeField]
    private Vector2 PositionOffset = Vector2.zero;
    [SerializeField]
    private Vector2 FireOffset = Vector2.zero;

    private GameObject gameObject;
    private SpriteRenderer spriteRenderer;
    private bool isFacingRight = true;

    public void Init(GameObject User)
    {
        gameObject = new GameObject("gun");
        gameObject.transform.parent = User.transform;
        gameObject.transform.localPosition = PositionOffset;

        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = Sprite;
        spriteRenderer.sortingOrder = 1;

    }

    public void UpdateLocalPosition(Vector3 originPosition, Vector3 targetPosition)
    {
        Vector3 aimDirection = (targetPosition - originPosition).normalized;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        gameObject.transform.eulerAngles = new Vector3(0, 0, angle);

        if (aimDirection.x > 0.01f)
        {
            isFacingRight = true;
            spriteRenderer.flipY = false;
            gameObject.transform.localPosition = PositionOffset;
        }
        if (aimDirection.x < 0.01f)
        {
            isFacingRight = false;
            spriteRenderer.flipY = true;
            gameObject.transform.localPosition = new Vector2(-PositionOffset.x, PositionOffset.y);
        }
    }

    public void Fire(Vector3 targetPosition)
    {
        Vector3 originPosition = (Quaternion.Euler(0f, 0f, gameObject.transform.eulerAngles.z) * FireOffset) + new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        if (!isFacingRight)
        {
            originPosition = (Quaternion.Euler(0f, 0f, gameObject.transform.eulerAngles.z) * new Vector2(FireOffset.x, -FireOffset.y)) + new Vector3(gameObject.transform.position.x, gameObject.transform.position.y);
        }

        Vector3 diff = targetPosition - originPosition;
        diff.Normalize();
        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        quaternion rot = Quaternion.Euler(0f, 0f, rot_z - 90);
        GameObject newBullet = Instantiate(Bullet, originPosition, rot);
        newBullet.GetComponent<Bullet>().Damage = BaseDamage;
        if (FireSound != null)
        {
            GameObject go = new GameObject();
            AudioSource FireSoundSource = go.AddComponent<AudioSource>();
            FireSoundSource.transform.localPosition = originPosition;
            FireSoundSource.clip = FireSound;
            FireSoundSource.Play();
            Destroy(go, 1);
        }
    }
}

