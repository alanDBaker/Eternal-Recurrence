using UnityEngine;
using System.Collections;

public class LFEnemyHealthBar : MonoBehaviour
{
    public LeftFaceEnemyAI Enemy;
    public Transform ForegroundSprite;
    public SpriteRenderer ForegroundRenderer;

    // green and red colors
    public Color MaxHealthColor = new Color(0 / 255f, 128 / 255f, 0 / 255f);
    public Color MinHealthColor = new Color(255 / 255f, 0 / 255f, 0 / 255f);


    // Update is called once per frame
    void Update()
    {
        // get the ratio of current health of player
        var healthPercent = Enemy.Health / (float)Enemy.MaxHealth;

        // set the heathPercent bar size
        ForegroundSprite.localScale = new Vector3(healthPercent, 1, 1);

        // set the current color
        ForegroundRenderer.color = Color.Lerp(MinHealthColor, MaxHealthColor, healthPercent);
    }

}

