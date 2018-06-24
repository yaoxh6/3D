using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NewMove : NetworkBehaviour
{
    public float moveSpeed;
    public float rotateSpeed;
    public void Start()
    {
        if (isLocalPlayer)
        {
            GameObject.Find("Main Camera").GetComponent<CameraFlow>().follow = gameObject.transform;
        }
    }
    public void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        MovePlayer(translationX, translationZ);
        Attack();
    }
    public void Attack()
    {
        //Fire1对应鼠标左键
        if (Input.GetButtonDown("Fire1"))
        {
            CmdAttack1();
        }
        //Fire2对应鼠标右键
        if (Input.GetButtonDown("Fire2"))
        {
            CmdAttack2();
        }
        //Jump对应空格键space
        if (Input.GetButtonDown("Jump"))
        {
            CmdJump();
        }
    }
    [Command]
    public void CmdAttack1()
    {
        RpcAttack1();
    }
    [ClientRpc]
    public void RpcAttack1()
    {
        this.GetComponent<Animator>().SetTrigger("attack1");
    }

    [Command]
    public void CmdAttack2()
    {
        RpcAttack2();
    }
    [ClientRpc]
    public void RpcAttack2()
    {
        this.GetComponent<Animator>().SetTrigger("attack2");
    }

    [Command]
    public void CmdJump()
    {
        RpcJump();
    }
    [ClientRpc]
    public void RpcJump()
    {
        this.GetComponent<Animator>().SetTrigger("jump");
    }

    public void MovePlayer(float translationX, float translationZ)
    {
        if (translationX != 0 || translationZ != 0)
        {
            this.GetComponent<Animator>().SetBool("run", true);
        }
        else
        {
            this.GetComponent<Animator>().SetBool("run", false);
        }

        this.transform.Translate(0, 0, translationZ * moveSpeed * Time.deltaTime);
        this.transform.Rotate(0, translationX * rotateSpeed * Time.deltaTime, 0);
        
        if (this.transform.localEulerAngles.x != 0 || this.transform.localEulerAngles.z != 0)
        {
            this.transform.localEulerAngles = new Vector3(0, this.transform.localEulerAngles.y, 0);
        }
        if (this.transform.position.y != 0)
        {
            this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        var hit = collision.gameObject;
        var hitPlayer = hit.GetComponent<NewMove>();

        if(hitPlayer != null)
        {
            Destroy(gameObject);
            Destroy(hit);
            GameObject.Find("GameObject").GetComponent<UserGUI>().isEnd = true;
        }
    }
}
