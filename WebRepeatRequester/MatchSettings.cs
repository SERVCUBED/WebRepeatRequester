using System.Collections.Generic;

namespace WebRepeatRequester
{
    public class MatchSettings
    {
        public bool ShouldMatch = false;

        public bool StopOnMatch = true;

        public Operator StopOnMatchOperator = Operator.OR;

        public List<MatchObject> MatchObjects = new List<MatchObject>();

        public enum Operator
        {
            AND,
            OR
        }
    }
}
