using System.Drawing.Text;
using UnityEngine;
using UnityEngine.AI;

public class LevelCreator : MonoBehaviour
{
    private const float FinishPosition = 0.85f;
    private const float OffSet = 2;
    private const int DecorationCount = 10;
    private const int DecorationSize = 1;
    private static readonly Vector3 LeftWallAngle = new Vector3(0, -90, 0);
    private static readonly Vector3 RightWallAngle = new Vector3(0, 90, 0);
    private static readonly Vector3 FarWallAngle = new Vector3(0, 180, 0);

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
            new Vector3(_platformWidth.GetHalf() , _boundHeight.GetHalf(), _boundWidth.GetHalf()),
            new Vector3(_platformWidth, _boundHeight, _boundWidth));
        SetUpBound(bound2,
            new Vector3(_platformWidth.GetHalf(), _boundHeight.GetHalf(), _platformLenght - _boundWidth.GetHalf()),
            new Vector3(_platformWidth, _boundHeight, _boundWidth));
        SetUpBound(bound3,
            new Vector3(_boundWidth.GetHalf(), _boundHeight.GetHalf(), _platformLenght.GetHalf()),
            new Vector3(_boundWidth, _boundHeight, _platformLenght));
        SetUpBound(bound4,
            new Vector3(_platformWidth - _boundWidth.GetHalf(), _boundHeight.GetHalf(), _platformLenght.GetHalf()),
            new Vector3(_boundWidth, _boundHeight, _platformLenght));
    }

    private void CreateFinish(Finish finishTemplate)
    {
        Finish = Instantiate(finishTemplate);
        Finish.transform.position = new Vector3(_platformWidth.GetHalf(), 0, _platformLenght * FinishPosition);
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
        const int blockCreationProbability = 20;

        for (float positionZ = rect.Point1.z; positionZ < rect.Point3.z; positionZ += size)
        {
            for (float positionX = rect.Point4.x; positionX <= rect.Point1.x; positionX += size)
            {
                int randomValue = Random.Range(0, blockCreationProbability);

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
        MyRectangle rect = new MyRectangle(new Vector3(_platformWidth + size * blocksCount + size.GetHalf(), -_platformHeight, _platformLenght + size),
            new Vector3(-size * blocksCount - size.GetHalf(), -_platformHeight, _platformLenght + size * blocksCount + size.GetHalf()));

        CreateBoxes(rect, size);
    }

    private void CreateWalls(Vector3 standPosition, Vector3 standScale)
    {
        CreateWall(new Vector3(-standScale.x.GetHalf() + standPosition.x, 0, standScale.z.GetHalf()),
            RightWallAngle,
            new Vector3(standScale.z, standScale.y, standScale.x));
        CreateWall(new Vector3(standScale.x.GetHalf() + standPosition.x, 0, standScale.z.GetHalf()),
            LeftWallAngle,
            new Vector3(standScale.z, standScale.y, standScale.x));

        CreateWall(new Vector3(standPosition.x, 0, standPosition.z + standScale.z.GetHalf()),
            FarWallAngle,
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
        const int SideDecorationsCount = 2;
        const int SideOffsetsCount = 4;
        const int ForwardOffsetCount = 2;

        float offsetY = 0.1f;

        _level = level;
        _platformWidth = level.Width;
        _platformHeight = level.Height;
        _platformLenght= level.Lenght;

        CreateGround(_level.Terrain);

        Vector3 standPosition = new Vector3(_level.Width.GetHalf(),
            -_level.Height.GetHalf() - offsetY,
            (_level.Lenght + ForwardOffsetCount * OffSet + DecorationSize * DecorationCount).GetHalf());

        Vector3 standScale = new Vector3(_level.Width + SideOffsetsCount * OffSet + SideDecorationsCount * DecorationSize * DecorationCount,
            _platformHeight,
            _level.Lenght + ForwardOffsetCount * OffSet + DecorationSize * DecorationCount);
        
        CreateStand(standPosition, standScale);
        CreateWalls(standPosition, standScale);

        CreateBounds(_level.Bound);
        CreateFinish(_level.Finish);
        CreateBackground();
    }
}

public static class FloatExtesion
{
    public static float GetHalf(this float value)
    {
        return value / 2f;
    }

    public static float GetHalf(this int value)
    {
        return value / 2f;
    }
}