import React from "react";
import Slider from '../Components/Slider';
import HomeCategories from '../Components/HomeCategories';
import TrendNews from '../Components/TrendNews';
import GetLastReviews from "../Components/GetLastReviews";
import RateList from "../Components/RateList";
import FutureGameList from "../Components/FutureGameList";

const Home = () => {
  return (
    <div className="container">
      <div className="mb-3 shadow">
        <Slider />
      </div>
      <div className="p-3 shadow">
        <HomeCategories />
      </div>
      <div className="p-3 mt-4 shadow">
        <h2 className="text-danger fw-bolder pb-4">Trend News</h2>
        <div className="row">
          <TrendNews />
        </div>
      </div>
      <div className="p-3 mt-4 shadow">
        <h2 className="text-danger fw-bolder pb-4">Last Reviews</h2>
        <div className="row">
          <GetLastReviews />
        </div>
      </div>
      <div className="row">
        <div className="p-3 mt-4">
          <div className="row">
            <div className="col-lg-6">
              <div className="shadow">
                <h2 className="text-danger fw-bolder p-4">Rate list</h2>
                <hr />
                <div className="container">
                  <h5>You can see the rate of any games that was released</h5>
                  <small className="text-secondary">These rate are gathering from Gamespot and IGN and etc.</small>
                  <hr />
                  <RateList/>
                </div>
              </div>
            </div>
            <div className="col-lg-6">
              <div className="shadow">
                <h2 className="text-danger fw-bolder p-4">Future list</h2>
                <hr />
                <div className="container">
                  <h5>You can see all the games that will be released</h5>
                  <small className="text-secondary">These rate are gathering from Gamespot and IGN and etc.</small>
                  <hr />
                  <FutureGameList/>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  )
};
export default Home;