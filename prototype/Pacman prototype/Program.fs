// Learn more about F# at http://fsharp.net

type gameObject = | Powerup
                  | Wall  
type size = int*int
type tile = {   x : int; 
                y : int;
                objects : (gameObject list) }

type gameMap =  tile list list

let createMap' (s : size) = 
    
    let rec f (t1) (t2) i j =
        if i = (fst s) && j = (snd s) then
            t2
        else if i = (fst s) then
            f [] (t1::t2) 0 (j+1)
        else
            f ((i,j)::t1) t2 (i+1) j
    
    f [] [] 0 0
;;

let createTile (x',y') = {x=x';y=y';objects = []}

let rec createMap ((h,w) : size) =
    match h with
    | 0 -> []
    | _ ->    (createMap (h-1,w))@[(List.map (fun (x',y') -> createTile (x',y')) (List.zip [1 .. w] ( List.replicate w h)))] 


