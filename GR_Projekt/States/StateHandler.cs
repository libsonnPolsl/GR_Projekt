using System.Collections.Generic;

namespace GR_Projekt.States
{
    public static class StateHandler
    {
        public static List<State> handleNewState(List<State> currentStates, State newState)
        {
            List<State> states = new List<State>();

            switch (newState.GetStateType) {
                case StateTypeEnumeration.Game:
                    foreach (State state in currentStates){
                        if (state.GetStateType != StateTypeEnumeration.Game) {
                            state.Dispose();
                        }
                    }
                    if (!currentStates.Exists((State state) => state.GetStateType == StateTypeEnumeration.Game))
                    {
                        states.Add(newState);
                    }
                    else {
                        return currentStates;
                    }
                    break;

                case StateTypeEnumeration.MainMenu:
                    foreach (State state in currentStates) {
                        state.Dispose();
                    }
                    states.Add(newState);
                    break;

                case StateTypeEnumeration.Pause:
                    states.Add(newState);
                    break;

                case StateTypeEnumeration.Settings:
                    states.Add(newState);
                    break;

                default:
                    states.Add(currentStates[currentStates.Count - 1]);
                    break;
            }
            return states;
        }
    }
}
