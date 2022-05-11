using UnityEngine;
using AC;

public class PointClickHorizontal2D : MonoBehaviour
{
    private void OnEnable()
    {
        DontDestroyOnLoad(this);
        EventManager.OnCharacterSetPath += OnCharacterSetPath; 
    }
    private void OnDisable () { EventManager.OnCharacterSetPath -= OnCharacterSetPath; }

    private void OnCharacterSetPath (AC.Char character, Paths path)
    {
        if (character == KickStarter.player)
        {
            if (path.nodes.Count > 1)
            {
                path.nodes.RemoveRange (0, path.nodes.Count - 2);
            }
            Vector3 lastNode = path.nodes[path.nodes.Count-1];
            lastNode.y = character.transform.position.y;
            path.nodes[path.nodes.Count-1] = lastNode;
        }
    }
		
}