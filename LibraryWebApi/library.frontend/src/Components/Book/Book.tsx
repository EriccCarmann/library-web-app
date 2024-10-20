import React from "react";
import "./Book.css";

type Props = {
    cover: string,
    title: string,
    genre: string,
    description: string,
    ISBN: string,
    author: string,
    available: string
};

const Book: React.FC<Props> = ({
    cover,
    title,
    genre,
    description,
    ISBN,
    author,
    available
}: Props): JSX.Element => {
    return (
        <div className="book">
            <p className="info">
                {available}
            </p>   
            <img
                src={cover}
                alt="Image"
            />
            <div className="details">
                <h2>{title}</h2>
                <p>{author}</p>
            </div>
            <p className="info">
                {description}
            </p>
            <p className="info">
                {genre}
            </p>
            <p className="info">
                {ISBN}
            </p>            
        </div>
    )
};

export default Book;