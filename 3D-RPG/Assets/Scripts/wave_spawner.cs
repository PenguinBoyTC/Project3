using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class wave_spawner : MonoBehaviour {

    public Transform enemy_prefab;
    public Transform spawn_point;

    public float time_between_waves = 5.5f;  //time between waves
    private float start_time_between_waves = 5.5f;
    private float countdown = 2f;  //time before first wave
    public float spawn_pause = 0.5f;  //time between spawns of enemies
    private int wave_index = 0;
    public Text wave_countdown_text;
    public int wave_max = 10;

    /**
    * Pre: wave_spawned object created
    * Post: counter adjusted and if timer has run out, spawn_wave called
    * return: NA
    **/
    void Update()
    {
        if (countdown <= 0f)
        {
            time_between_waves = spawn_pause * wave_index + start_time_between_waves;
            StartCoroutine(Spawn_wave());  //spawn wave
            countdown = time_between_waves; // reset time between waves
        }

        countdown -= Time.deltaTime;  //time ticks at one tick per second

        //wave_countdown_text.text = Mathf.Round(countdown).ToString();  //turns countdown into string after finding floor of countdown, aka, not decimals 
    }

    /**
    * Pre: countdown timer expired
    * Post: spawn_enemy called for the wave
    * return: NA
    **/
    IEnumerator Spawn_wave()  //allows pausing of spawn_wave
    {
        if (wave_index < wave_max)
        {
            wave_index++;
            for (int i = 0; i < wave_index; i++)
            {
                Spawn_enemy();
                yield return new WaitForSeconds(spawn_pause);  //pauses before spawning new enemy
            }
        }
    }

    /**
    * Pre: none
    * Post: Enemy object created at spawn point
    * return: NA
    **/
    void Spawn_enemy()
    {
        int spawnIndex = Random.Range(0, waypoints.points.Length - 1);
        Instantiate(enemy_prefab, waypoints.points[spawnIndex].position, waypoints.points[spawnIndex].rotation);
    }

}
