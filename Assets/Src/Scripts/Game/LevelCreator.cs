using UnityEngine;
using UnityEngine.AI;

public class LevelCreator : MonoBehaviour
{
    private const float FinishPosition = 0.85f;

    [SerializeField] private GameObject _stand;
    [SerializeField] private float _boundWidth;
    [SerializeField] private float _boundHeight;
    [SerializeField] private NavMeshSurface _navMesh;

    private int _platformWidth;
    private int _platformLenght;
    private int _platformHeight;
    private TerrainController _terrainController;

    public Finish Finish { get; private set; }
    public TerrainController TerrainController => _terrainController;

    private void CreateGround(Terrain template)
    {
        var createdTerrain = Instantiate(template);
        createdTerrain.terrainData.size = new Vector3(_platformWidth, _platformHeight, _platformLenght);
        _navMesh.BuildNavMesh();

        _terrainController = createdTerrain.GetComponent<TerrainController>();
    }

    private void CreateBounds(GameObject boundTemplate)
    {
        var bound1 = Instantiate(boundTemplate);
        var bound2 = Instantiate(boundTemplate);
        var bound3 = Instantiate(boundTemplate);
        var bound4 = Instantiate(boundTemplate);
        SetUpBound(bound1,
            new Vector3(_platformWidth / 2, _boundHeight / 2, _boundWidth / 2),
            new Vector3(_platformWidth, _boundHeight, _boundWidth));
        SetUpBound(bound2,
            new Vector3(_platformWidth / 2, _boundHeight / 2, _platformLenght - _boundWidth / 2),
            new Vector3(_platformWidth, _boundHeight, _boundWidth));
        SetUpBound(bound3,
            new Vector3(_boundWidth / 2, _boundHeight / 2, _platformLenght / 2),
            new Vector3(_boundWidth, _boundHeight, _platformLenght));
        SetUpBound(bound4,
            new Vector3(_platformWidth - _boundWidth / 2, _boundHeight / 2, _platformLenght / 2),
            new Vector3(_boundWidth, _boundHeight, _platformLenght));
    }

    private void CreateFinish(Finish finishTemplate)
    {
        Finish = Instantiate(finishTemplate);
        Finish.transform.position = new Vector3(_platformWidth / 2, 0, _platformLenght * FinishPosition);
    }

    private void SetUpBound(GameObject bound, Vector3 position, Vector3 scale)
    {
        bound.transform.position = position;
        bound.transform.localScale = scale;
    }

    private void CreateStand(Vector3 position, Vector3 scale)
    {
        GameObject stand = Instantiate(_stand);

        stand.transform.localScale = scale;
        stand.transform.position = position;
    }

    private void CreateBackground()
    {
        const float maxSideHeight = 20;
        const float minSideHeight = 15;
        const float maxFarHeight = 40;
        const float minFarHeight = 25;

        float size = 2;
        int blocksCount = 5;

        CreateSideBoxes(size, blocksCount, minSideHeight, maxSideHeight);
        CreateFarBoxes(size, blocksCount, minFarHeight, maxFarHeight);
    }

    private void CreateBoxes(MyRectangle rect, float size, float minHeight, float maxHeight)
    {
        for (float positionZ = rect.Point1.z; positionZ < rect.Point3.z; positionZ += size)
        {
            for (float positionX = rect.Point4.x; positionX <= rect.Point1.x; positionX += size)
            {
                int randomValue = Random.Range(0, 4);

                if (randomValue == 0)
                {
                    float scaleY = Random.Range(minHeight, maxHeight);
                    float positionY = -_platformHeight;

                    CreateStand(new Vector3(positionX, positionY, positionZ),
                        new Vector3(size, scaleY, size));
                }
            }
        }
    }

    private void CreateFarBoxes(float size, int blocksCount, float minHeight, float maxHeight)
    {
        MyRectangle rect = new MyRectangle(new Vector3(_platformWidth + size * blocksCount + size / 2, -_platformHeight, _platformLenght + size),
            new Vector3(-size * blocksCount - size / 2, -_platformHeight, _platformLenght + size * blocksCount + size / 2));

        CreateBoxes(rect, size, minHeight, maxHeight);
    }

    private void CreateSideBoxes(float size, int blocksCount, float minHeight, float maxHeight)
    {
        MyRectangle leftSide = new MyRectangle(new Vector3(-size / 2, -_platformHeight, size / 2),
            new Vector3(-size * blocksCount - size / 2, -_platformHeight, _platformLenght + size / 2));
        MyRectangle rightSide = new MyRectangle(new Vector3(_platformWidth + size * blocksCount + size / 2, -_platformHeight, size / 2),
            new Vector3(_platformWidth + size / 2, -_platformHeight, _platformLenght + size / 2));

        CreateBoxes(leftSide, size, minHeight, maxHeight);
        CreateBoxes(rightSide, size, minHeight, maxHeight);
    }

    public void Create(Terrain terrainTemplate, GameObject boundTemplate, Finish finishTemplate, int platformWidth, int platformLenght, int platformHeight)
    {
        _platformWidth = platformWidth;
        _platformHeight = platformHeight;
        _platformLenght = platformLenght;

        CreateGround(terrainTemplate);
        CreateStand(new Vector3(_platformWidth / 2, -platformHeight / 2f - 0.1f, _platformLenght / 2),
            new Vector3(_platformWidth, platformHeight, _platformLenght));
        CreateBounds(boundTemplate);
        CreateFinish(finishTemplate);
        CreateBackground();
    }
}
