using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts
{
    class TestFindWay
    {
        class Building
        {
            public string name;
            public Dictionary<Building, float> ways = new Dictionary<Building, float>();
        }

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
        void run()
        {
            List<Building> buildings = new List<Building>();

            Building A = new Building(), B = new Building(), C = new Building(), D = new Building(), E = new Building(), F = new Building();

            A.name = "A";
            A.ways.Add(B, 0.5f);
            A.ways.Add(C, 1f);
            A.ways.Add(D, 6f);

            B.name = "B";
            B.ways.Add(A, 0.5f);
            B.ways.Add(F, 10f);
            B.ways.Add(D, 1f);


            C.name = "C";
            C.ways.Add(A, 1f);
            C.ways.Add(E, 5f);

            D.name = "D";
            D.ways.Add(A, 6f);
            D.ways.Add(E, 1f);
            D.ways.Add(B, 1f);

            E.name = "E";
            E.ways.Add(C, 5f);
            E.ways.Add(D, 1f);

            F.name = "F";
            F.ways.Add(B, 10f);


            buildings.Add(A);
            buildings.Add(B);
            buildings.Add(C);
            buildings.Add(D);
            buildings.Add(E);

            var start = A;
            var end = E;
            //           index ,  cha
            Dictionary<Building, TwoValue> tree = new Dictionary<Building, TwoValue>();
            Dictionary<Building, TwoValue> temp = new Dictionary<Building, TwoValue>();
            var index = start;
            tree.Add(A, new TwoValue(null, 0));
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
                //checkList[currentMin.Key] = true;

                if (tree.Count >= buildings.Count || temp.Count == 0)
                    break;


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


                foreach (var e in result)
                {
                    Console.WriteLine(e.name);
                }
                Console.Write("");
            }
            else
            {
                Console.WriteLine("khong tin thay");
            }
        }
    }
}

