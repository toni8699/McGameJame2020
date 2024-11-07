using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHandler : MonoBehaviour
{
    public GameObject bullet;

    private List<GameObject> bullets = new List<GameObject>();

    void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            //GameObject tempBullet = Instantiate(bullet, Vector2.zero, Quaternion.identity, transform);
            GameObject tempBullet = Instantiate(bullet);
            tempBullet.SetActive(false);
            bullets.Add(tempBullet);
        }
    }


    public void ShootBullet(Vector2 position, Vector2 direction, GameObject parent)
    {
        foreach (GameObject currBullet in bullets) // Looping through bullets to find free one (not in use/unactive)
        { 
            if (!currBullet.activeSelf)
            {
                currBullet.transform.position = new Vector3(position.x, position.y, 15);
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
             
                currBullet.GetComponent<BulletController>().Fire(direction, parent);
                return;
            }
            
        }

        // Creating new bullet if there are no free bullets
        GameObject tempBullet = Instantiate(bullet, Vector2.zero, Quaternion.identity, transform);
        tempBullet.transform.position = new Vector3(position.x, position.y, 15);
        bullets.Add(tempBullet);
        tempBullet.transform.position = position;
        tempBullet.GetComponent<BulletController>().Fire(direction, parent);
    }
}
