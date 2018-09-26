using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController _controller;
    [SerializeField]
    private float _speed = 5.0f;
    private float _gravity = 9.81f;
    [SerializeField]
    private GameObject _muzzleFlash;
    [SerializeField]
    private GameObject _hitMarkerPrefab;
    [SerializeField]
    private AudioSource _weaponAudio;
    [SerializeField]
    private int currentAmmo = 0;
    private int maxAmmo = 50;
    private bool _isReloading = false;
    private UIManager _uiManager;

    public bool hasCoin = false;
    public bool hasWeapon = false;

    [SerializeField]
    private GameObject _weapon;

    // Use this for initialization
    void Start ()
    {
        _controller = GetComponent<CharacterController>();
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        CalculateMovement();
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
         
        if (Input.GetMouseButton(0) && currentAmmo > 0 && hasWeapon)
        {
            Shoot();
        }
        else
        {
            _muzzleFlash.SetActive(false);
            _weaponAudio.Pause();
        }

        if (!_isReloading && Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmo)
        {
            _isReloading = true;
            StartCoroutine(Reload());
        }
        _uiManager.UpdateAmmo(currentAmmo);
        _uiManager.UpdateInventory(hasCoin);
    }

    public void EnableWeapons()
    {
        hasWeapon = true;
        currentAmmo = maxAmmo;
        _weapon.SetActive(true);
    }

    IEnumerator Reload()
    {
        yield return new WaitForSeconds(1.5f);
        currentAmmo = maxAmmo;
        _uiManager.UpdateAmmo(currentAmmo);
        _isReloading = false;
    }

    public void Shoot()
    {
        _muzzleFlash.SetActive(true);
        currentAmmo--;

        //_uiManager.UpdateAmmo(currentAmmo);

        if (!_weaponAudio.isPlaying)
        {
            _weaponAudio.Play();
        }
        Ray rayOrigin = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hitInfo;

        if (Physics.Raycast(rayOrigin, out hitInfo, Mathf.Infinity))
        {
            Debug.Log("Hit: " + hitInfo.transform.name);
            GameObject hitMarker = Instantiate(_hitMarkerPrefab, hitInfo.point, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
            Destroy(hitMarker, 0.11f);

            Destructible crate = hitInfo.transform.GetComponent<Destructible>();
            if (crate != null)
            {
                crate.DestroyCrate();
            }
        }
    }

    public void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector3 direction = new Vector3(horizontalInput, 0, verticalInput);
        Vector3 velocity = direction * _speed;
        velocity.y -= _gravity;

        velocity = transform.TransformDirection(velocity);
        _controller.Move(velocity * Time.deltaTime);
    }
}
