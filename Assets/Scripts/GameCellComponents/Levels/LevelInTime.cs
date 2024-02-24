using UnityEngine;

public class LevelInTime : LevelBase
{

    public int timeInSeconds;
    public int targetScore;

    private float timer;
    private bool timeIsOver;

    private void Start()
    {
        type = LevelType.CLEAR_IN_TIME;

        Debug.Log("Time:" + timeInSeconds + "second. Target score:" + targetScore);
    }


    private void Update()
    {
        if (!timeIsOver)
        {

            timer += Time.deltaTime;
            if (timeInSeconds - timer <= 0)
            {
                if (currentScore >= targetScore)
                {
                    GameWin();
                }
                else
                {
                    GameLose();
                }

                timeIsOver = true;
            }

        }
    }

}
