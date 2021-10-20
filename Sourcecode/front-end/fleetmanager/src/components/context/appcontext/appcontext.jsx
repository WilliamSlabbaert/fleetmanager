import React, { createContext ,useState} from "react";

export const AppContext = createContext();

export const AppProvider = ({ children }) => {
  const [menuName, setMenuName] = useState(["menuBtn", "menuList"]);

    return (
    <AppContext.Provider value={{menuName,setMenuName}}>
        {children}
    </AppContext.Provider>)
}
