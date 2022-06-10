using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.Experimental.Playables;
using System.Threading;
//using System.Numerics;
using UnityEngine.SceneManagement;
using UnityEditor.UIElements;

public class SpawnBall : MonoBehaviour
{
    [SerializeField]
	GameObject ball;
    [SerializeField]
	Text ScoreText;
    [SerializeField]
    Collider TargetColl;
    [SerializeField]
    GameObject ParticleShow;
    //[SerializeField]
   // GameObject ballParent;

   public bool SwipeOn;
   public Button SpawnButton;
   public int score;
   public static SpawnBall instance;
   public GameObject pointsAdd;
   public Text highScore;
   public Text timer;
   public float seconds = 20;
    public float miliseconds = 0;
    public bool startGame;
    public GameObject TimerImage;
    public bool oneTime;
    public GameObject GameOverCanvas;
    public GameObject MenuCanvas;
    public GameObject SettingsCanvas;
    public bool removeTimer;
    public bool OnceMoreButton;
    public GameObject GameParent;
   private void Awake() {
       highScore.text = PlayerPrefs.GetInt("HighScore",0).ToString();
       instance = this;
       score = 0;
       SwipeOn = true;
       OnceMoreButton = true;
   }

   void Update(){
       print(pointsAdd.transform.position);
      if(startGame && !removeTimer){
          /* if (oneTime){
               oneTime = false;
               TimerImage.transform.DOScale(new Vector3(0.026f,0.026f,0.026f),1f);
           }*/
        if(miliseconds <= 0){
           
           
        if(seconds <= (0)){
            startGame = false;
            GameOver();
            return;
        }
        else if(seconds >= 0){
            seconds--;
        }
        miliseconds = 100;
        }
    miliseconds -= Time.deltaTime * 100;
    timer.text = string.Format("{0}:{1}", seconds, (int)miliseconds);
    }
    }

    public void TimerOff()
    {
        removeTimer = true;
    }

    public void TimerOn()
    {
        removeTimer = false;
    }

    public void OnceMoreOn(){
        OnceMoreButton = true;
    }

    public void OnceMoreOff(){
        OnceMoreButton = false;
    }


    public void SwipeEnable(){
       SwipeOn = true;
   }

     public void SwipeDisable(){
       SwipeOn = false;
   }
    private GameObject newBall;

	public void Spawn()
	{
        print("Spawning ball");
        TargetColl.enabled = true;
        SwipeOn = true;
		//newBall  = Instantiate (ball, new Vector3(-0.83f, 0.885f, 3.01f), Quaternion.identity);
       
        newBall  = Instantiate (ball, new Vector3(-0.42f, 0.8f, 3.01f), Quaternion.identity);
         newBall.transform.SetParent(GameParent.transform);
         newBall.transform.localPosition = new Vector3(-0.42f, 0.8f, 3.01f);
        newBall.transform.rotation = Quaternion.Euler(-90,0,0);
        
       // newBall.transform.SetParent(ballParent.transform);
        //newBall.transform.localScale = new Vector3 (0.05f,0.05f,0.05f);
        print("Swipe On : "+SwipeOn);
	}

    public void InceaseScore(){
        ParticleShow.SetActive(true);
        TargetColl.enabled = false;
        pointsAdd.transform.localScale = new Vector3(1,1,1);
        pointsAdd.transform.DOMove(new Vector3(900, 700, 0),2f);
        pointsAdd.transform.DOScale(new Vector3(0, 0, 0),2f).OnComplete(resetPos);
        //pointsAdd.transform.position = new Vector3(971, 400, 0);
        score++;
        ScoreText.text = ""+score;
        if (score> PlayerPrefs.GetInt("HighScore",0)){
            PlayerPrefs.SetInt("HighScore", score);
        }
        
    }

    public void resetHighScore(){
        PlayerPrefs.DeleteAll();
    }

    void resetPos(){
        pointsAdd.transform.position = new Vector3(971, 400, 0);
        ParticleShow.SetActive(false);
    }

    void GameOver(){
        MenuCanvas.transform.GetComponent<GraphicRaycaster>().enabled = false;
        SettingsCanvas.SetActive(false);
        GameOverCanvas.SetActive(true);
        SwipeOn = false;
    }

    public void Restart(){  
        SceneManager.LoadScene(0);
    }

    public void Quit(){
        #if !UNITY_EDITOR
        Application.Quit();
        #else
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
