using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FindWay : MonoBehaviour
{

    class TwoValue
    {
        public Building fatherBuilding;
        public float lenghFromStart;
        public TwoValue(Building b, float l)
        {
            fatherBuilding = b;
            lenghFromStart = l;
        }
    }
    List<Building> buildings;

    // Start is called before the first frame update
    void Awake()
    {
        buildings= new List<Building>(FindObjectsOfType<Building>());
    }

    public List<Building> getWays(Building start, Building end)
    {
        Dictionary<Building, TwoValue> tree = new Dictionary<Building, TwoValue>();
        Dictionary<Building, TwoValue> temp = new Dictionary<Building, TwoValue>();
        var index = start;
        tree.Add(index, new TwoValue(null, 0));
        //Dictionary<Building, bool> checkList = new Dictionary<Building, bool>();



        while (!tree.ContainsKey(end))
        {
            foreach (var element in index.ways)
            {
                if (!temp.ContainsKey(element.Key) && !tree.ContainsKey(element.Key))
                {
                    temp.Add(element.Key, new TwoValue(index, tree[index].lenghFromStart + element.Value));
                }
            }

            var currentMin = temp.Aggregate((l, r) => l.Value.lenghFromStart < r.Value.lenghFromStart ? l : r);

            if (tree.Count >= buildings.Count || temp.Count == 0)
                break;

            UpdateWays(tree, temp, index);

            temp.Remove(currentMin.Key);

            try
            {
                if (currentMin.Value.lenghFromStart > tree[index].lenghFromStart + index.ways[currentMin.Key])
                    tree.Add(currentMin.Key, new TwoValue(index, tree[index].lenghFromStart + index.ways[currentMin.Key]));
                else
                    tree.Add(currentMin.Key, new TwoValue(currentMin.Value.fatherBuilding, currentMin.Value.lenghFromStart));
            }
            catch
            {
                tree.Add(currentMin.Key, new TwoValue(currentMin.Value.fatherBuilding, currentMin.Value.lenghFromStart));
            }
            index = currentMin.Key;


        }

        return findWaysFromTree(end, tree);


    }

    private List<Building> findWaysFromTree(Building end, Dictionary<Building, TwoValue> tree)
    {
        List<Building> result = new List<Building>();
        if (tree.ContainsKey(end))
        {
            var i = end;
            while (i != null)
            {

                result.Add(i);
                i = tree[i].fatherBuilding;
            }

            result.Reverse();

            return result;
            //foreach (var e in result)
            //{
            //    Console.WriteLine(e.name);
            //}
            //Console.Write("");
        }
        else
        {
            return null;
            //Console.WriteLine("khong tin thay");
        }
    }

    private void UpdateWays(Dictionary<Building, TwoValue> tree, Dictionary<Building, TwoValue> temp, Building index)
    {
        foreach (var element in temp)
        {
            if (element.Key.ways.ContainsKey(index))
            {
                if (tree[index].lenghFromStart + element.Key.ways[index] < element.Value.lenghFromStart)
                {
                    element.Value.fatherBuilding = index;
                    element.Value.lenghFromStart = tree[index].lenghFromStart + element.Key.ways[index];
                }

            }
        }
    }
}
