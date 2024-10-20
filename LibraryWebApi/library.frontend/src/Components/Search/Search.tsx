import React, { ChangeEvent, MouseEvent } from "react";

interface Props {
    search: string | undefined;
    handleChange: (e: ChangeEvent<HTMLInputElement>) => void;
    onClick: (e: MouseEvent<HTMLButtonElement, MouseEvent>) => void;
};

const Search: React.FC<Props> = ({
    search,
    handleChange,
    onClick
}: Props): JSX.Element => {
    return (
        <div>
            <input value={search} onChange={(e) => handleChange(e)}></input>
            <button onClick={(e) => onClick(e)}></button>
        </div>
    )
};

export default Search;