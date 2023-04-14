using UnityEngine;
using UnityEngine.AI;

public class LevelCreator : MonoBehaviour
{
    private const float FinishPosition = 0.85f;
    private const float OffSet = 2;
    private const int DecorationCount = 10;
    private const int DecorationSize = 1;

    [SerializeField] private float _boundWidth;
    [SerializeField] private float _boundHeight;
    [SerializeField] private NavMeshSurface _navMesh;

    private int _platformWidth;
    private int _platformLenght;
    private int _platformHeight;
    private TerrainController _terrainController;
    private Level _level;

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
            new Vector3(_platformWidth / 2f, _boundHeight / 2f, _boundWidth / 2f),
            new Vector3(_platformWidth, _boundHeight, _boundWidth));
        SetUpBound(bound2,
            new Vector3(_platformWidth / 2f, _boundHeight / 2f, _platformLenght - _boundWidth / 2f),
            new Vector3(_platformWidth, _boundHeight, _boundWidth));
        SetUpBound(bound3,
            new Vector3(_boundWidth / 2f, _boundHeight / 2f, _platformLenght / 2f),
            new Vector3(_boundWidth, _boundHeight, _platformLenght));
        SetUpBound(bound4,
            new Vector3(_platformWidth - _boundWidth / 2f, _boundHeight / 2f, _platformLenght / 2f),
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
        GameObject stand = Instantiate(_level.Stand);

        stand.transform.localScale = scale;
        stand.transform.position = position;
    }

    private void CreateBackground()
    {
        CreateSideDecorations(DecorationSize, DecorationCount);
        CreateFarDecorations(DecorationSize, DecorationCount);
    }

    private void CreateSideDecorations(float size, int blocksCount)
    {
        MyRectangle leftSide = new MyRectangle(new Vector3(-OffSet, 0, 0),
            new Vector3(-OffSet - size * blocksCount, 0, _platformLenght + size));

        MyRectangle rightSide = new MyRectangle(new Vector3(_platformWidth + OffSet + size * blocksCount, 0, 0),
            new Vector3(_platformWidth + OffSet, 0, _platformLenght));

        CreateBoxes(leftSide, size);
        CreateBoxes(rightSide, size);
    }

    private void CreateBoxes(MyRectangle rect, float size)
    {
        for (float positionZ = rect.Point1.z; positionZ < rect.Point3.z; positionZ += size)
        {
            for (float positionX = rect.Point4.x; positionX <= rect.Point1.x; positionX += size)
            {
                int randomValue = Random.Range(0, 20);

                if (randomValue == 0)
                {
                    CreateDecoration(new Vector3(positionX, 0, positionZ));
                }
            }
        }
    }

    private void CreateDecoration(Vector3 position)
    {
        int decorationId = Random.Range(0, _level.Decorations.Length);
        GameObject decoration = Instantiate(_level.Decorations[decorationId]);

        decoration.transform.position = position;
    }

    private void CreateFarDecorations(float size, int blocksCount)
    {
        MyRectangle rect = new MyRectangle(new Vector3(_platformWidth + size * blocksCount + size / 2, -_platformHeight, _platformLenght + size),
            new Vector3(-size * blocksCount - size / 2, -_platformHeight, _platformLenght + size * blocksCount + size / 2));

        CreateBoxes(rect, size);
    }

    private void CreateWalls(Vector3 standPosition, Vector3 standScale)
    {
        CreateWall(new Vector3(-standScale.x / 2 + standPosition.x, 0, standScale.z / 2),
            new Vector3(0, 90, 0),
            new Vector3(standScale.z, standScale.y, standScale.x));
        CreateWall(new Vector3(standScale.x / 2 + standPosition.x, 0, standScale.z / 2),
            new Vector3(0, -90, 0),
            new Vector3(standScale.z, standScale.y, standScale.x));

        CreateWall(new Vector3(standPosition.x, 0, standPosition.z + standScale.z / 2),
            new Vector3(0, 180, 0),
            new Vector3(standScale.x, standScale.y, 1));
    }

    private void CreateWall(Vector3 position,Vector3 angle, Vector3 scale)
    {
        GameObject wall = Instantiate(_level.Wall);

        wall.transform.position = position;
        wall.transform.rotation = Quaternion.Euler(angle);
        wall.transform.localScale = scale;
    }

    public void Create(Level level)
    {
        float offsetY = 0.1f;

        _level = level;
        _platformWidth = level.Width;
        _platformHeight = level.Height;
        _platformLenght= level.Lenght;

        CreateGround(_level.Terrain);

        Vector3 standPosition = new Vector3(_level.Width / 2,
            -_level.Height / 2f - offsetY,
            (_level.Lenght + 2 * OffSet + DecorationSize * DecorationCount) / 2);

        Vector3 standScale = new Vector3(_level.Width + 4 * OffSet + 2 * DecorationSize * DecorationCount,
            _platformHeight,
            _level.Lenght + 2 * OffSet + DecorationSize * DecorationCount);
        
        CreateStand(standPosition, standScale);
        CreateWalls(standPosition, standScale);

        CreateBounds(_level.Bound);
        CreateFinish(_level.Finish);
        CreateBackground();
    }
}
