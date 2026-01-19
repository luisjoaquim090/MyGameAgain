namespace Console.Utils
{
    internal class Args
    {
        public enum ArgTypes
        {
            Title,
            gridX,
            gridY,
            gridZ
        }

        internal static List<Tuple<ArgTypes,object>> ProcessArgs(string[] args)
        {
            var r_args = new List<Tuple<ArgTypes, object>>();
            for (int i = 0; i < args.Length; i++)
            {
                string key = args[i].Split("=")[0];
                string value = args[i].Split("=")[1];
                switch (key)
                {
                    case "title":
                        r_args.Add(new Tuple<ArgTypes, object>(ArgTypes.Title, value));
                        break;
                    case "gridX":
                        r_args.Add(new Tuple<ArgTypes, object>(ArgTypes.gridX, int.Parse(value)));
                        break;
                    case "gridY":
                        r_args.Add(new Tuple<ArgTypes, object>(ArgTypes.gridY, int.Parse(value)));
                        break;
                    case "gridZ":
                        r_args.Add(new Tuple<ArgTypes, object>(ArgTypes.gridZ, int.Parse(value)));
                        break;
                    default:
                        break;
                }
            }
            return r_args;
        }
    }
}
