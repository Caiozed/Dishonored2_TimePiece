using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Camera cameraA;
    public Camera cameraB;
    public float Speed;
    public RenderTexture renderTextureA, renderTextureB;
    public GameObject MirrorA, MirrorB;
    Animator animator;
    float rotX, rotY;
    CharacterController characterController;
    // Start is called before the first frame update
    void Start()
    {
        cameraA.depth = 1;
        cameraB.depth = -1;
        cameraA.targetTexture = null;
        characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        rotX += Input.GetAxis("Mouse X");
        rotY += Input.GetAxis("Mouse Y");
        float x = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        Vector3 invertedDir = transform.TransformDirection(new Vector3(x, 0, y));
        characterController.Move(invertedDir);
        cameraA.transform.eulerAngles = new Vector3(rotY * 1.5f, rotX * 1.5f, 0);


        if (Input.GetButtonDown("Fire1"))
        {
            animator.SetTrigger("LevelChange");
        }
    }

    public void LevelChange()
    {
        if (cameraA.targetTexture)
        {
            cameraB.targetTexture = renderTextureB;
            cameraA.targetTexture = null;
            MirrorA.SetActive(true);
            MirrorB.SetActive(false);
        }
        else
        {
            cameraB.targetTexture = null;
            cameraA.targetTexture = renderTextureA;
            MirrorA.SetActive(false);
            MirrorB.SetActive(true);
        }

        cameraA.depth = cameraA.depth * -1;
        cameraB.depth = cameraB.depth * -1;
    }
}
