using Mond;

namespace TIKSN.Leveret.Interpretation.MondConcretion.Factories
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