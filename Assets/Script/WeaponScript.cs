﻿using UnityEngine;
using System.Collections;

public class WeaponScript : MonoBehaviour {
	
	public enum WeaponType
	{
		SABER,
		UZI,
		SHOTGUN,
		SNIPER,
		DARK,
	}
	
	[HideInInspector]
	public int 				ammo = 0;
	public AudioClip		audioShoot;
	
	public WeaponType		weaponType = 0;
	private	float			power = 0;
	private Vector3			dropDir;
	private GameObject		shoot;
	private float			fireRate = 1;
	private AudioClip		audioNoAmmo;
	
	// Use this for initialization
	void Start () {
		if (weaponType == WeaponType.SABER) {
			shoot = Resources.Load("Prefabs/WeaponShoot/SaberShoot") as GameObject;
		} else if (weaponType == WeaponType.UZI) {
			shoot = Resources.Load("Prefabs/WeaponShoot/UziShoot") as GameObject;
		} else if (weaponType == WeaponType.SHOTGUN) {
			shoot = Resources.Load("Prefabs/WeaponShoot/ShotgunShoot") as GameObject;
		} else if (weaponType == WeaponType.SNIPER) {
			shoot = Resources.Load("Prefabs/WeaponShoot/SniperShoot") as GameObject;
		} else if (weaponType == WeaponType.DARK) {
			shoot = Resources.Load("Prefabs/WeaponShoot/DarkShoot") as GameObject;
		}
		initAmmo ();
		audioNoAmmo = Resources.Load ("Audio/Sounds/dry_fire") as AudioClip;
	}
	
	// Update is called once per frame
	void Update () {
		if (power > 0) {
			transform.position = Vector2.MoveTowards (transform.position, dropDir, Time.deltaTime * power);
			power =- 0.1f;
		}
	}
	
	public void DoDropWeapon (Vector3 playerPos) {
		gameObject.GetComponent<SpriteRenderer> ().enabled = true;
		gameObject.GetComponent<BoxCollider2D> ().enabled = true;
		transform.parent = null;
		transform.position = playerPos;
		power = 125;
		Vector3 mousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
		dropDir = mousePos;	
	}

	void initAmmo() {
		if (weaponType == WeaponType.SABER) {
			ammo = -1;
			fireRate = 0.5f;
		} else if (weaponType == WeaponType.UZI) {
			ammo = 150;
			fireRate = 0.1f;
		} else if (weaponType == WeaponType.SHOTGUN) {
			ammo = 50;
			fireRate = 1;
		} else if (weaponType == WeaponType.SNIPER) {
			ammo = 30;
			fireRate = 3;
		} else if (weaponType == WeaponType.DARK) {
			ammo = -1;
			fireRate = 0.1f;
		}
	}

	public void DoTakeWeapon (GameObject obj) {
		transform.parent = obj.transform;
		gameObject.GetComponent<SpriteRenderer> ().enabled = false;
		gameObject.GetComponent<BoxCollider2D> ().enabled = false;
		if (obj.tag == "enemie")
			ammo = -1;
		else
			initAmmo ();
	}
	
	public IEnumerator DoShoot(GameObject master) {
		Vector3 pos;
		Vector3 dir;
		if (ammo == 0)
			AudioSource.PlayClipAtPoint (audioNoAmmo, transform.position);
		while (ammo != 0) {
			if (ammo != -1)
				ammo -= 1;
			AudioSource.PlayClipAtPoint(audioShoot, transform.position);
			dir = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).normalized;
			pos = transform.position + (dir * 2);
			GameObject obj = Instantiate(shoot, transform.position, Quaternion.identity) as GameObject;
			if (weaponType == WeaponType.SABER)
				obj.GetComponent<ShootScript>().InitShoot(dir, false, 0, pos);
			else
				obj.GetComponent<ShootScript>().InitShoot(dir, true, 0, pos);
			yield return new WaitForSeconds (fireRate);
		}
	}

	public IEnumerator DoShootEnemy(GameObject master, GameObject target) {
		Vector3 pos;
		Vector3 dir;
		while (ammo != 0) {
			if (ammo != -1)
				ammo -= 1;
			AudioSource.PlayClipAtPoint(audioShoot, transform.position);	
			dir = (target.transform.position - master.transform.position).normalized;
			pos = transform.position + (dir * 2);
			GameObject obj = Instantiate(shoot, transform.position, Quaternion.identity) as GameObject;
			if (weaponType == WeaponType.SABER)
				obj.GetComponent<ShootScript>().InitShoot(dir, false, 1, pos);
			else
				obj.GetComponent<ShootScript>().InitShoot(dir, true, 1, pos);
			yield return new WaitForSeconds (fireRate);
		}
	}

}
