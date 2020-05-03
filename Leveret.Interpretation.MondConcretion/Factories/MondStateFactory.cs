using Mond;

namespace TIKSN.Leveret.Interpretation.MondConcretion
{
    public class MondStateFactory : IMondStateFactory
    {
        public MondState Create()
        {
            var state = new MondState()
            {
                Options = new MondCompilerOptions()
                {
                    MakeRootDeclarationsGlobal = true,
                    UseImplicitGlobals = true
                }
            };

            return state;
        }
    }
}