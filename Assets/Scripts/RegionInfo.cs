using UnityEngine;
using System.Collections;

public class RegionInfo : MonoBehaviour {


	public GameObject[,] regionMap = new GameObject[512,512];
	public GameObject[,] regionTiles = new GameObject[10,10];
	public GameObject[] tileList = new GameObject[4];

	public int regionPositionX;
	public int regionPositionY;
	public bool visible;
	public bool drawn;

	void Start ()
	{
		//Types of tiles
		tileList[0] = transform.parent.GetComponent<RegionCreator>().pfbGrassTile;	
		tileList[1] = transform.parent.GetComponent<RegionCreator>().pfbForestTile;



		for(int x = 0; x < 10; x++)
		{
			for(int y = 0; y < 10; y++)
			{
				//The 2nd number should equal the amount of different tiles
				var random = Random.Range(0, 2);
				regionMap[x,y] = tileList[random];
			}
		}
	}

	void Update()
	{
		if(visible == true && drawn == false)
		{
			Draw();
		}
		else if(visible == false && drawn == true)
		{
			for(int x = 0; x < 10; x++)
			{
				for(int y = 0; y < 10; y++)
				{
					Destroy(regionTiles[x,y]);
				}
			}
			drawn = false;
		}
		if(visible == true && drawn == true)
		{
			CheckWhetherStillUpToDate();
		}
	}

	void Draw()
	{
		for(int x = 0; x < 10; x++)
		{
			for(int y = 0; y < 10; y++)
			{
				var curr = Instantiate(regionMap[x,y], new Vector3(regionPositionX*10+x, regionPositionY*10+y), Quaternion.identity) as GameObject;
				regionTiles[x,y] = curr;
			}
		}
		drawn = true;
	}

	void CheckWhetherStillUpToDate()
	{
		var worldInfo = transform.parent.GetComponent<WorldInfo>();
		if(regionPositionX > worldInfo.currCameraPositionX-2 || regionPositionX < worldInfo.currCameraPositionX+2 || regionPositionY > worldInfo.currCameraPositionY-2 || regionPositionY < worldInfo.currCameraPositionY+2)
		{
			visible = false;
		}
	}
}
