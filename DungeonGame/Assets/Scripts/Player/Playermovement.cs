using UnityEngine;
using UnityEngine.SceneManagement;

public class Playermovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D rb;
    public Weapon weapon;

    Vector2 moveDirection;
    Vector2 mousePosition;

    void Update()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
<<<<<<< HEAD
        if (Input.GetKey(KeyCode.Escape))
        {
            //TODO: Open pause menu 
        }
        if (Input.GetKey(KeyCode.I))
        {
            SceneManager.LoadScene("SkillTree");
        }
=======


        moveDirection = new Vector2(moveX, moveY).normalized;
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

>>>>>>> 640deef4b759cb7e37306fecd9f2450bb9fcf401
        if(Input.GetMouseButtonDown(0))

        {
            weapon.Fire();
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = new Vector2(moveDirection.x * speed, moveDirection.y * speed);

        Vector2 aimDirection = mousePosition - rb.position;
        float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;

        rb.rotation = aimAngle;
    }
}
