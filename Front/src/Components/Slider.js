import react, { useEffect, useState } from 'react';
import axios from 'axios';
import { Link } from 'react-router-dom';
import { BrowserRouter, Route } from 'react-router-dom';
import Cookies from 'universal-cookie';
export default function Slider() {

    const [sliderApi, setSliderApi] = useState([]);
    const [Loading, setLoading] = useState(true);
    const BaseImageUrl = "https://localhost:44352/SliderImages/";
    const cookies = new Cookies();
    const token="Bearer "+ cookies.get('Token');
    const config = { headers: { 'Content-Type': 'multipart/form-data' ,'Authorization': token} };
    useEffect(() => {
        axios.get("https://localhost:44352/api/slider/",config).then((response) => {
            getData(response.data)
        }).catch((err) => err.name);

    }, [])

    const getData = (data) => {
        setLoading(true)
        setSliderApi(data)
        setTimeout(function () {
            setLoading(false)
        }, 1000);
    }

    return (
        <div id="carouselExampleDark" className="carousel carousel-dark  slide" data-bs-ride="carousel">
            <div className="carousel-inner">
                {
                    Loading == true ?
                        <div className="row align-content-center">
                            <div className="d-flex justify-content-center p-5">
                                <div className="spinner-border text-info spinner-size" role="status">
                                    <span className="visually-hidden">Loading...</span>
                                </div>

                            </div>
                                <div className="d-flex justify-content-center p-5">
                                    Please wait
                                </div>
                            </div>
                        :
                        sliderApi.map((data) => {
                            return (
                                <div key={data.id} className={data.id % 2 === 0 ? "carousel-item" : "carousel-item active"} data-bs-interval="10000">
                                    <img src={BaseImageUrl + data.image} className="img-fluid d-block w-100" height="500" alt={data.caption} />
                                    <div className="carousel-caption d-none d-md-block">
                                        <div className={data.id % 2 === 0 ? "shadow-lg bg-dark opacity-75 rounded float-start" : "shadow-lg bg-dark opacity-75 rounded float-end"}>
                                            <div className="p-3">
                                                <h1 className="text-light display-1">{data.caption}</h1>
                                                <h2 className="text-light">{data.about}</h2>
                                                <p className="pt-3 pb-3"><a href={data.link == null ? "#" : data.link} className="btn btn-sm btn-light">Keep going</a></p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            )
                        })
                }
            </div>
            <button className="carousel-control-prev" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="prev">
                <span className="carousel-control-prev-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Previous</span>
            </button>
            <button className="carousel-control-next" type="button" data-bs-target="#carouselExampleDark" data-bs-slide="next">
                <span className="carousel-control-next-icon" aria-hidden="true"></span>
                <span className="visually-hidden">Next</span>
            </button>
        </div>
    )
}




