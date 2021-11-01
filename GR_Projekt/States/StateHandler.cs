using System.Collections.Generic;

namespace GR_Projekt.States
{
    public static class StateHandler
    {
        public static List<State> handleNewState(List<State> currentStates, State newState)
        {
            switch (newState.GetStateType) {
                case StateTypeEnumeration.Game:
                    for (int i = 0; i < currentStates.Count; i++)
                    {
                        State state = currentStates[i];
                        if (state.GetStateType != StateTypeEnumeration.Game) {
                              
                            state.Dispose();
                            currentStates.Remove(state);
                        }
                    }
                    if (!currentStates.Exists((State state) => state.GetStateType == StateTypeEnumeration.Game))
                    {
                        currentStates.Add(newState);
                    }
                    else {
                        return currentStates;
                    }
                    break;

                case StateTypeEnumeration.MainMenu:
                    for (int i = 0; i < currentStates.Count; i++)
                    {
                        currentStates[i].Dispose();
                        currentStates.Remove(currentStates[i]);
                    }
                    currentStates.Add(newState);
                    break;

                case StateTypeEnumeration.Pause:
                    currentStates.Add(newState);
                    break;

                case StateTypeEnumeration.Settings:
                    currentStates.Add(newState);
                    break;

                default:
                    currentStates.Add(currentStates[currentStates.Count - 1]);
                    break;
            }
            return currentStates;
        }
    }
}
