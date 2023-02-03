using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class history{
  List<GameObject> obj;
  Vector2 dir;
  public history(List<GameObject> o, Vector2 d){
      obj=o;
      dir=d;
  }
  public void Retornar(){
     foreach(GameObject block in obj){
            block.GetComponent<Snaper>().PushTo(-dir, true);
        }  
  }
}
public class MoveHistory : MonoBehaviour
{
    // Start is called before the first frame update
    Stack<history> historyList = new Stack<history>();

    void Start()
    {
         EventManager.HL += AddToHistory;
    }

    // Update is called once per frame

    public void AddToHistory(List<GameObject> block, Vector2 direction){
      // HL.Add(block,direction);
      historyList.Push(new history(block,direction));

    }
    void Update()
    {
   
      if(Input.GetKeyDown(KeyCode.V)&&historyList.Count>0){
        history poping = historyList.Pop();
        poping.Retornar();
        
      }
    }
}
